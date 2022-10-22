using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeType shapeType;
        [SerializeField] private ShapeAngle shapeAngle;

        private bool isSelectable = true;
        public bool IsSelectable { get => isSelectable; }
        private bool isSelected = false;
        public bool IsSelected { get => isSelected; }
        private ShapeOutline outline;
        public ShapeOutline Outline { get => outline; }
        public ShapeType ShapeType { get => shapeType; set => shapeType = value; }
        public ShapeAngle ShapeAngle { get => shapeAngle; set => shapeAngle = value; }
        public GameEventSO SelectedEvent { get => selectedEvent; set => selectedEvent = value; }

        private GameEventSO selectedEvent;

        private void Start() {
            outline = GetComponentInChildren<ShapeOutline>();
        }

        public void Select()
        {
            isSelected = true;
            outline?.Enable();
            selectedEvent?.Raise();
        }

        public void Deselect()
        {
            isSelected = false;
            outline?.Disable();
        }

        public void SetNotSelectable()
        {
            isSelectable = false;
        }

        public void Move(Vector3 direction)
        {
            transform.position = direction;
        }

        public void SetSelectable()
        {
            isSelectable = true;
        }
    }
}
