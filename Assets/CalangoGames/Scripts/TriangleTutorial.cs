using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class TriangleTutorial : MonoBehaviour
    {

        [SerializeField] private List<GameObject> arrows;
        [SerializeField] private Shape firstShape;
        [SerializeField] private Shape secondShape;

        private List<Shape> shapesInScene;
        private int currentStep = 0;
        // Start is called before the first frame update
        void Start()
        {
            foreach(GameObject go in arrows)
            {
                go.SetActive(false);
            }
            StartArrowTutorial();
        }

        public void StartArrowTutorial()
        {
            SetAllShapesNotSelectable();
            ShowArrow0();
            SetShapeSelectable(firstShape);
        }

        private void SetShapeSelectable(Shape shape)
        {
            shape.SetSelectable();
            shapesInScene.Remove(shape);
        }

        private void SetAllShapesNotSelectable()
        {
            shapesInScene = new List<Shape>();
            var shapeArray = Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[];
            foreach (Shape shape in shapeArray)
            {
                shapesInScene.Add(shape);
                shape.SetNotSelectable();
            }
        }


        private void ShowArrow0()
        {
            arrows[0].SetActive(true);
            currentStep = 0;
        }

        public void ShowArrow1()
        {
            arrows[0].SetActive(false);
            arrows[1].SetActive(true);
            currentStep = 1;
        }
        public void ShowArrow2orEndTutorial()
        {
            if(currentStep == 1)
            {
                ShowArrow2();
            }
            else
            {
                EndTutorial();
            }    
        }

        private void EndTutorial()
        {
            gameObject.SetActive(false);
            SetRemainingShapesSelectable();
        }

        private void SetRemainingShapesSelectable()
        {
            foreach(Shape shape in shapesInScene)
            {
                shape.SetSelectable();
            }
        }

        private void ShowArrow2()
        {
            arrows[1].SetActive(false);
            arrows[2].SetActive(true);
            currentStep = 2;
            SetShapeSelectable(secondShape);
        }
        public void ShowArrow3()
        {
            arrows[2].SetActive(false);
            arrows[3].SetActive(true);
            currentStep = 3;
        }
    }
}
