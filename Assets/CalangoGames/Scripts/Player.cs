using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class Player : MonoBehaviour
    {
        private Shape selectedShape;

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
