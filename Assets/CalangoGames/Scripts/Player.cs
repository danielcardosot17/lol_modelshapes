using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CalangoGames
{
    public class Player : MonoBehaviour
    {
        private Shape selectedShape;
        private InputActions inputActions;
        private InputActionAsset asdasdas;
        private InputAction clickAction;
        private InputAction tapAction;
        private InputAction touchAction;
        private InputAction touchPosition;
        private InputAction mousePosition;
        private bool isTouching = false;

        private Camera mainCamera;

        private void Awake() {
            Debug.Log("Awake");
            mainCamera = Camera.main;
            inputActions = new InputActions();
            clickAction = inputActions.Player.Click;
            tapAction = inputActions.Player.Tap;
            touchAction = inputActions.Player.Touch;
            touchPosition = inputActions.Player.TouchPosition;
            mousePosition = inputActions.Player.MousePosition;
        }

        private void OnEnable() {
            Debug.Log("OnEnable");
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
            Debug.Log("OnTouchEnd");
        }

        private void OnTouchStart(InputAction.CallbackContext obj)
        {
            isTouching = true;
            Debug.Log("OnTouchStart");
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            Debug.Log("OnClick");
            ClickOrTapToRay(Mouse.current.position.ReadValue());
        }

        private void OnTap(InputAction.CallbackContext obj)
        {
            Debug.Log("OnTap");
            ClickOrTapToRay(touchPosition.ReadValue<Vector2>());
        }

        private void ClickOrTapToRay(Vector3 screenPoint)
        {
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if(hit2D.collider != null) // hit something
            {
                if(hit2D.collider.gameObject.GetComponent<Shape>() != null) // is a Shape!
                {
                    SelectShape(hit2D.collider.gameObject.GetComponent<Shape>());
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
            
            if(selectedShape.ShapeType == shapeSlot.ShapeType)
            {
                shapeSlot.Occupy();
                DeselectShape();
            }

        }
        public void DeselectShape()
        {
            selectedShape.Deselect();
            selectedShape = null;
        }
    }
}
