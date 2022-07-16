using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class Shape : MonoBehaviour
    {
        private bool isSelected = false;

        public bool IsSelected { get => isSelected; }

        public void Select()
        {
            isSelected = true;
        }
    }
}
