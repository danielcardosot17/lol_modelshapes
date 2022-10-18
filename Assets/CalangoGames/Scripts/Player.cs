using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CalangoGames
{
    public class Player : MonoBehaviour
    {
        [SerializeField][Range(0.1f, 2f)] private float moveSmoothTime = 0.5f;
        [SerializeField][Range(0.1f, 2f)] private float moveShapeDuration = 0.5f;
        private Shape selectedShape;
        private InputActions inputActions;
        private InputAction clickAction;
        private InputAction tapAction;
        private InputAction touchAction;
        private InputAction touchPosition;
        private InputAction mousePosition;
        private AudioManager audioManager;
        private bool isTouching = false;

        private Camera mainCamera;

        private void Awake() {
            mainCamera = Camera.main;
            inputActions = new InputActions();
            clickAction = inputActions.Player.Click;
            tapAction = inputActions.Player.Tap;
            touchAction = inputActions.Player.Touch;
            touchPosition = inputActions.Player.TouchPosition;
            mousePosition = inputActions.Player.MousePosition;
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void OnEnable() {
            clickAction.performed += OnClick;
            tapAction.performed += OnTap;
            touchAction.performed += OnTouchStart;
            touchAction.canceled += OnTouchEnd;
            inputActions.Player.Enable();
        }

        private void OnDisable() {
            clickAction.performed -= OnClick;
            tapAction.performed -= OnTap;
            touchAction.performed -= OnTouchStart;
            touchAction.canceled -= OnTouchEnd;
            inputActions.Player.Disable();
        }

        private void OnTouchEnd(InputAction.CallbackContext obj)
        {
            isTouching = false;
        }

        private void OnTouchStart(InputAction.CallbackContext obj)
        {
            isTouching = true;
            ClickOrTapToRay(touchPosition.ReadValue<Vector2>());
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            ClickOrTapToRay(Mouse.current.position.ReadValue());
        }

        private void OnTap(InputAction.CallbackContext obj)
        {
        }

        private void ClickOrTapToRay(Vector3 screenPoint)
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

            GetFirstSelectableShapeOrFreeSlot(ray);
        }

        private void GetFirstSelectableShapeOrFreeSlot(Ray ray)
        {
            RaycastHit2D[] hit2Darray = Physics2D.GetRayIntersectionAll(ray);
            foreach(RaycastHit2D hit2D in hit2Darray)
            {
                if (hit2D.collider.gameObject.GetComponent<Shape>() != null) // is a Shape!
                {
                    var shape = hit2D.collider.gameObject.GetComponent<Shape>();
                    SelectShape(shape);
                    if (shape.IsSelected)
                    {
                        StartCoroutine(DragUpdate(hit2D.collider));
                        return;
                    }
                }
                else if (hit2D.collider.gameObject.GetComponent<ShapeSlot>() != null) // is a ShapeSlot!
                {
                    SelectSlot(hit2D.collider.gameObject.GetComponent<ShapeSlot>());
                }
            }
        }

        private IEnumerator DragUpdate(Collider2D collider)
        {
            Vector3 velocity = Vector3.zero;
            Ray ray = new Ray();
            
            while(clickAction.ReadValue<float>() != 0 || isTouching)
            {
                if(isTouching)
                {
                    ray = mainCamera.ScreenPointToRay(touchPosition.ReadValue<Vector2>());
                }
                else{
                    ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                }
                Vector3 direction = Vector3.ProjectOnPlane(ray.origin, Vector3.forward);
                collider.transform.position = Vector3.SmoothDamp(collider.transform.position, direction, ref velocity, moveSmoothTime);
                yield return null;
            }
            
            DropShape(collider);
        }

        private void DropShape(Collider2D collider) // check if dropped over Slot
        {
            List<Collider2D> overlappingColliders = new List<Collider2D>();
            if(collider.OverlapCollider(new ContactFilter2D().NoFilter(), overlappingColliders) > 0) // there is an overlapping collider
            {
                foreach(var otherCollider in overlappingColliders)
                {
                    if(otherCollider.gameObject.GetComponent<ShapeSlot>() != null) // is a ShapeSlot!
                    {
                        SelectSlot(otherCollider.gameObject.GetComponent<ShapeSlot>());
                    }
                }
            }
        }

        public void SelectShape(Shape shape)
        {
            if(shape == null) return;
            if(shape == selectedShape) return;
            if(!shape.IsSelectable) return;
            // Another shape was selected
            if(selectedShape != null)
            {
                selectedShape.Deselect(); // Deselect current shape
            }
            selectedShape = shape; // Change selected Shape
            selectedShape.Select(); // Select shape
        }


        public void SelectSlot(ShapeSlot shapeSlot)
        {
            if(shapeSlot == null) return;
            if(shapeSlot.IsOccupied) return;
            if(selectedShape == null) return;
            
            if(selectedShape.ShapeType == shapeSlot.ShapeType && selectedShape.ShapeAngle == shapeSlot.ShapeAngle)
            {
                Shape shape = selectedShape;
                StartCoroutine(MoveShapeToSlot(shape, shapeSlot));
                selectedShape.SetNotSelectable();
                DeselectShape();
            }
        }

        private IEnumerator MoveShapeToSlot(Shape shape, ShapeSlot shapeSlot)
        {
            float time = 0;
            Vector3 startPosition = shape.transform.position;
            while (time < moveShapeDuration)
            {
                shape.transform.position = Vector3.Lerp(startPosition, shapeSlot.transform.position, time / moveShapeDuration);
                time += Time.deltaTime;
                yield return null;
            }
            shape.transform.position = shapeSlot.transform.position;

            // occupy only after moved
            shapeSlot.Occupy();

            if (shape.ShapeType == ShapeType.Clay)
            {
                audioManager.PlayStickOrClaySFX("Clay");
            }
            else
            {
                audioManager.PlayStickOrClaySFX("Stick");
            }
        }

        public void DeselectShape()
        {
            selectedShape.Deselect();
            selectedShape = null;
        }

        public void DisablePlayerInput()
        {
            inputActions.Player.Disable();
        }
        public void EnablePlayerInput()
        {
            inputActions.Player.Enable();
        }
    }
}
