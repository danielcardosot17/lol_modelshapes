using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
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
            outline.Enable();
        }

        public void Deselect()
        {
            isSelected = false;
        }

        public void SetNotSelectable()
        {
            isSelectable = false;
        }
        public void SetSelectable()
        {
            isSelectable = true;
        }
    }
}
