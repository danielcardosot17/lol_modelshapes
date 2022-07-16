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
            selectedShape = shape;
            shape.Select();
        }
    }
}
