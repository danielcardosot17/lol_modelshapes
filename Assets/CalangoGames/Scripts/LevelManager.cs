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
        [SerializeField] private GameEventSO levelFinishedEvent;
        public GameEventSO occupyEvent;

        private bool isLoading = false;
        private int currentLevelIndex = 0;
        private Level currentLevel;
        public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }

        private bool isEnd = false;
        public bool IsEnd { get => isEnd; }
        public List<Shape> ShapesInScene { get => shapesInScene; set => shapesInScene = value; }

        public void StartLevelAnimation()
        {

        }

        public List<ShapeSlot> SlotsInScene { get => slotsInScene; set => slotsInScene = value; }
        public int NumberOfShapes { get => numberOfShapes; set => numberOfShapes = value; }
        public int NumberOfSlots { get => numberOfSlots; set => numberOfSlots = value; }
        public int NumberOfOccupiedSlots { get => numberOfOccupiedSlots; set => numberOfOccupiedSlots = value; }

        private List<Shape> shapesInScene;
        private List<ShapeSlot> slotsInScene;

        private int numberOfShapes = 0;
        private int numberOfSlots = 0;
        private int numberOfOccupiedSlots = 0;

        private void Awake() {
            currentLevel = levels[0];
        }

        public IEnumerator LoadLevel(Level level)
        {
            // start transition
            isLoading = true;
            transitionAnimator.SetBool("isLoading", isLoading);
            yield return new WaitForSeconds(transitionTime);

            StartCoroutine(WaitForLevelToBeLoaded(level));
        }

        private IEnumerator WaitForLevelToBeLoaded(Level level)
        {
            if (!level.IsLoaded)
            {
                var asyncLoad = SceneManager.LoadSceneAsync(level.name, LoadSceneMode.Additive);
                while(!asyncLoad.isDone)
                {
                    Debug.Log("Loading level");
                    yield return null;
                }
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
            Debug.Log("slotsInScene.Count");
            Debug.Log(slotsInScene.Count);
            foreach (var slot in slotsInScene)
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
            var nextIndex = currentLevelIndex + 1;
            if (nextIndex >= levels.Count)
            {
                isEnd = true;
                return;
            }
            var currentLevel = levels[currentLevelIndex];
            currentLevelIndex++;
            var nextLevel = levels[currentLevelIndex];

            UnLoadLevel(currentLevel);

            StartCoroutine(LoadLevel(nextLevel));

        }

        public bool IsLastLevel()
        {
            if(currentLevelIndex == (levels.Count - 1))
            {
                return true;
            }
            return false;
        }

        public void UpdateOccupiedSlots()
        {
            numberOfOccupiedSlots++;
            if(numberOfOccupiedSlots == numberOfShapes)
            {
                currentLevel.IsFinished = true;
                levelFinishedEvent.Raise();
            }
        }
    }
}
