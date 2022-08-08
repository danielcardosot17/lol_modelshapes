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
        public string levelName;
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

        private TextManager textManager;

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
        public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
        public List<Level> Levels { get => levels; set => levels = value; }


        private List<Shape> shapesInScene;
        private List<ShapeSlot> slotsInScene;

        private int numberOfShapes = 0;
        private int numberOfSlots = 0;
        private int numberOfOccupiedSlots = 0;

        private void Awake() {
            currentLevel = levels[0];
            textManager = FindObjectOfType<TextManager>();
        }

        public void StartLevelAnimation()
        {

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
                var asyncLoad = SceneManager.LoadSceneAsync(level.levelName, LoadSceneMode.Additive);
                while(!asyncLoad.isDone)
                {
                    yield return null;
                }
                level.IsLoaded = true;
            }

            isLoading = false;
            transitionAnimator.SetBool("isLoading", isLoading);
            FindShapesInScene();
            FindSlotsInScene();
            SetSlotsOccupyEvent();
            textManager.UpdateLevelNameText(level.shapeText);
            //textManager.CancelText();
            textManager.SpeakText(level.shapeText.ToLower());
        }
        private void SetSlotsOccupyEvent()
        {
            foreach (var slot in slotsInScene)
            {
                slot.OccupyEvent = occupyEvent;
            }
        }

        public void FindSlotsInScene()
        {
            slotsInScene = new List<ShapeSlot>();
            var slotArray = Resources.FindObjectsOfTypeAll(typeof(ShapeSlot)) as ShapeSlot[];
            //foreach (ShapeSlot shapeSlot in slotArray)
            //{
            //    if (!EditorUtility.IsPersistent(shapeSlot.transform.root.gameObject) && !(shapeSlot.hideFlags == HideFlags.NotEditable || shapeSlot.hideFlags == HideFlags.HideAndDontSave))
            //        slotsInScene.Add(shapeSlot);
            //}
            //numberOfSlots = slotsInScene.Count;
            foreach(ShapeSlot shapeSlot in slotArray)
            {
                slotsInScene.Add(shapeSlot);
            }
            numberOfSlots = slotArray.Length;
            numberOfOccupiedSlots = 0;
        }

        public void FindShapesInScene()
        {
            shapesInScene = new List<Shape>();
            var shapeArray = Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[];
            //foreach (Shape shape in shapeArray)
            //{
            //    if (!EditorUtility.IsPersistent(shape.transform.root.gameObject) && !(shape.hideFlags == HideFlags.NotEditable || shape.hideFlags == HideFlags.HideAndDontSave))
            //        shapesInScene.Add(shape);
            //}
            //numberOfShapes = shapesInScene.Count;
            foreach (Shape shape in shapeArray)
            {
                shapesInScene.Add(shape);
            }
            numberOfShapes = shapeArray.Length;
        }

        public IEnumerator UnLoadLevel(Level level)
        {
            if (level.IsLoaded)
            {
                AsyncOperation ao = SceneManager.UnloadSceneAsync(level.levelName);
                yield return ao;
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
            var levelToUnload = currentLevel;
            StartCoroutine(UnLoadLevel(levelToUnload));
            currentLevelIndex++;
            currentLevel = levels[currentLevelIndex];
            var nextLevel = levels[currentLevelIndex];
            StartCoroutine(LoadLevel(nextLevel));

        }

        public void RestartLevel()
        {
            StartCoroutine(UnLoadLevel(currentLevel));
            StartCoroutine(LoadLevel(currentLevel));
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

        public void SpeakLevelName()
        {
            textManager.SpeakText(currentLevel.shapeText.ToLower());
        }
    }
}
