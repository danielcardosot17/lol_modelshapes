using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public Sprite exampleImageSprite;
        public Color imageColor;

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
        [SerializeField] private Image exampleImage;

        private TextManager textManager;

        public GameEventSO occupyEvent;

        private bool isLoading = false;

        private int currentLevelIndex = 0;
        public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }

        private Level currentLevel;
        public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }

        private bool isEnd = false;
        public bool IsEnd { get => isEnd; }
        public List<Shape> ShapesInScene { get => shapesInScene; set => shapesInScene = value; }

        public List<ShapeSlot> SlotsInScene { get => slotsInScene; set => slotsInScene = value; }
        public int NumberOfShapes { get => numberOfShapes; set => numberOfShapes = value; }
        public int NumberOfSlots { get => numberOfSlots; set => numberOfSlots = value; }
        public int NumberOfOccupiedSlots { get => numberOfOccupiedSlots; set => numberOfOccupiedSlots = value; }
        public List<Level> Levels { get => levels; set => levels = value; }
        public IAnimationManager AnimationManager { get => sceneAnimationManager; set => sceneAnimationManager = value; }

        private IAnimationManager sceneAnimationManager;

        private List<Shape> shapesInScene;
        private List<ShapeSlot> slotsInScene;

        private int numberOfShapes = 0;
        private int numberOfSlots = 0;
        private int numberOfOccupiedSlots = 0;
        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
            currentLevel = levels[0];
            textManager = FindObjectOfType<TextManager>();
        }

        public void StartLevelAnimation()
        {
            HideShapesAndSlots();
            sceneAnimationManager.ShowFinishedShape();
            sceneAnimationManager.StartShapeAnimation();
            sceneAnimationManager.StartBackgroundAnimation();
            sceneAnimationManager.MoveCamera();
            sceneAnimationManager.StartSFX();
        }

        private void HideShapesAndSlots()
        {
            foreach(var slot in slotsInScene)
            {
                slot.gameObject.SetActive(false);
            }
            foreach (var shape in shapesInScene)
            {
                shape.gameObject.SetActive(false);
            }
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

            sceneAnimationManager = FindObjectsOfType<MonoBehaviour>().OfType<IAnimationManager>().ToArray()[0];

            StartCoroutine(DoAfterTimeCoroutine(1, () => {
                textManager.UpdateLevelNameText(level.shapeText);
                textManager.SpeakText(level.shapeText.ToLower());
                UpdateShapeExampleImage(level);
            }));
        }

        private void UpdateShapeExampleImage(Level level)
        {
            exampleImage.sprite = level.exampleImageSprite;
            exampleImage.color = level.imageColor;
        }

        public void ResetCameraPositionAndSize()
        {
            mainCamera.transform.position = -1 * Vector3.forward;
            mainCamera.orthographicSize = 20;
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
            Debug.Log("currentLevelIndex");
            Debug.Log(currentLevelIndex);
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
        public static IEnumerator DoAfterTimeCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
