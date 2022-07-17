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

        public void Select()
        {
            isSelected = true;
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
