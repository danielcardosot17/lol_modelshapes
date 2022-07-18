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
        private bool isSelectable = true;
        public bool IsSelectable { get => isSelectable; }
        private bool isSelected = false;
        public bool IsSelected { get => isSelected; }
        private ShapeOutline outline;
        public ShapeOutline Outline { get => outline; }

        private void Start() {
            outline = GetComponentInChildren<ShapeOutline>();
        }

        public void Select()
        {
            isSelected = true;
            outline?.Enable();
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
