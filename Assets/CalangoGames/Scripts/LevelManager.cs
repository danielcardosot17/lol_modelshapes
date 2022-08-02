using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CalangoGames
{
    [System.Serializable]
    public class Level
    {
        public string name;
        public string shapeText;
        public Vector2 finalCameraPosition;
        public float finalCameraSize;
        private bool isLoaded = false;
        public bool IsLoaded { get => isLoaded; set => isLoaded = value; }
        private bool isFinished = false;
        public bool IsFinished { get => isFinished; set => isFinished = value; }
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private Animator transitionAnimator;
        [SerializeField] private float transitionTime = 1f;
        public GameEventSO occupyEvent;

        private bool isLoading = false;
        private int currentLevelIndex = 0;
        private Level currentLevel;
        public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }

        private bool isEnd = false;
        public bool IsEnd { get => isEnd; }
        public List<Shape> ShapesInScene { get => shapesInScene; set => shapesInScene = value; }
        public List<ShapeSlot> SlotsInScene { get => slotsInScene; set => slotsInScene = value; }
        public int NumberOfShapes { get => numberOfShapes; set => numberOfShapes = value; }
        public int NumberOfSlots { get => numberOfSlots; set => numberOfSlots = value; }
        public int NumberOfOccupiedSlots { get => numberOfOccupiedSlots; set => numberOfOccupiedSlots = value; }

        private List<Shape> shapesInScene;
        private List<ShapeSlot> slotsInScene;

        private int numberOfShapes = 0;
        private int numberOfSlots = 0;
        private int numberOfOccupiedSlots = 0;


        private void Start()
        {
            StartCoroutine(LoadLevel(levels[0]));
        }

        IEnumerator LoadLevel(Level level)
        {
            // start transition
            isLoading = true;
            transitionAnimator.SetBool("isLoading", isLoading);
            yield return new WaitForSeconds(transitionTime);
            if (!level.IsLoaded)
            {
                SceneManager.LoadScene(level.name, LoadSceneMode.Additive);
                level.IsLoaded = true;
            }

            isLoading = false;
            transitionAnimator.SetBool("isLoading", isLoading);
            FindShapesInScene();
            FindSlotsInScene();
            SetSlotsOccupyEvent();
        }

        private void SetSlotsOccupyEvent()
        {
            foreach(var slot in slotsInScene)
            {
                slot.OccupyEvent = occupyEvent;
            }
        }

        public void FindSlotsInScene()
        {
            slotsInScene = new List<ShapeSlot>();

            foreach (ShapeSlot shapeSlot in Resources.FindObjectsOfTypeAll(typeof(ShapeSlot)) as ShapeSlot[])
            {
                if (!EditorUtility.IsPersistent(shapeSlot.transform.root.gameObject) && !(shapeSlot.hideFlags == HideFlags.NotEditable || shapeSlot.hideFlags == HideFlags.HideAndDontSave))
                    slotsInScene.Add(shapeSlot);
            }
            numberOfSlots = slotsInScene.Count;
            numberOfOccupiedSlots = 0;
        }

        public void FindShapesInScene()
        {
            shapesInScene = new List<Shape>();

            foreach (Shape shape in Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[])
            {
                if (!EditorUtility.IsPersistent(shape.transform.root.gameObject) && !(shape.hideFlags == HideFlags.NotEditable || shape.hideFlags == HideFlags.HideAndDontSave))
                    shapesInScene.Add(shape);
            }
            numberOfShapes = shapesInScene.Count;
        }

        private void UnLoadLevel(Level level)
        {
            if (level.IsLoaded)
            {
                SceneManager.UnloadSceneAsync(level.name);
                level.IsLoaded = false;
            }
        }

        public void LoadNextLevel()
        {
            var currentLevel = levels[currentLevelIndex];
            currentLevelIndex++;
            if (currentLevelIndex >= levels.Count)
            {
                isEnd = true;
                return;
            }
            var nextLevel = levels[currentLevelIndex];

            UnLoadLevel(currentLevel);

            StartCoroutine(LoadLevel(nextLevel));

        }

        public void UpdateOccupiedSlots()
        {
            numberOfOccupiedSlots++;
            if(numberOfOccupiedSlots == numberOfShapes)
            {
                currentLevel.IsFinished = true;
            }
        }
    }
}
