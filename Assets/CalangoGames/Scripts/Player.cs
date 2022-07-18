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
        private InputAction selectAction;
        private InputAction singleTouchAction;
        private InputAction touchPositionAction;
        private InputAction mousePositionAction;
        private bool isTouching = false;

        private Camera mainCamera;

        private void Awake() {
            Debug.Log("Awake");
            mainCamera = Camera.main;
            inputActions = new InputActions();
            selectAction = inputActions.Player.Select;
            singleTouchAction = inputActions.Player.SingleTouch;
            touchPositionAction = inputActions.Player.TouchPosition;
            mousePositionAction = inputActions.Player.MousePosition;
        }

        private void OnEnable() {
            Debug.Log("OnEnable");
            selectAction.performed += Select;
            singleTouchAction.performed += OnTouchStart;
            singleTouchAction.canceled += OnTouchEnd;
            inputActions.Player.Enable();
        }

        private void OnDisable() {
            selectAction.performed -= Select;
            singleTouchAction.performed -= OnTouchStart;
            singleTouchAction.canceled -= OnTouchEnd;
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

        private void Select(InputAction.CallbackContext obj)
        {
            Debug.Log("Select");
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

        public void DeselectShape(Shape shape)
        {
            selectedShape = null;
            shape.Deselect();
        }
    }
}
