using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField] private GameObject congratulationsCanvas;
        [SerializeField] private GameObject endgameCanvas;
        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private GameObject tutorialCanvas;
        [SerializeField] private GameObject optionsMenuCanvas;
        [SerializeField] private GameObject restartGameConfirmationCanvas;
        [SerializeField] private GameObject exampleCanvas;
        [SerializeField] private GameObject shapesTable;
        [SerializeField] private GameEventSO updateTextEvent;

        private LevelManager levelManager;
        private LOLAdapter lolAdapter;
        private Player player;
        private AudioManager audioManager;
        private TextManager textManager;

        private void Awake()
        {
#if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
#endif
            levelManager = FindObjectOfType<LevelManager>();
            audioManager = FindObjectOfType<AudioManager>();
            textManager = FindObjectOfType<TextManager>();
            lolAdapter = new LOLAdapter();
            lolAdapter.UpdateTextEvent = updateTextEvent;
            player = FindObjectOfType<Player>();
        }
        // Start is called before the first frame update
        void Start()
        {
            HideOptionsMenuCanvas();
            HidePauseGameCanvas();
            HideTutorialCanvas();
            HideEndGameCanvas();
            HideCongratulationCanvas();
            HideRestartGameConfirmationCanvas();

            lolAdapter.Initialize();
            lolAdapter.SetGameReady();
            StartCoroutine(lolAdapter.WaitForApproval());

            audioManager.PlayMusic("Music");

            StartGame();
        }

        private void StartGame()
        {
            lolAdapter.LoadGame<SaveData>(OnLoad);
            StartCoroutine(levelManager.LoadLevel(levelManager.CurrentLevel));
        }

        public void RestartLevel()
        {
            levelManager.RestartLevel();
            //StartCoroutine(levelManager.UnLoadLevel(levelManager.CurrentLevel));
            //StartGame();
        }


        public void NewGame()
        {
            var levelIndex = levelManager.CurrentLevelIndex;
            StartCoroutine(levelManager.UnLoadLevel(levelManager.Levels[levelIndex]));
            levelManager.CurrentLevelIndex = 0;
            levelManager.CurrentLevel = levelManager.Levels[0];
            StartCoroutine(levelManager.LoadLevel(levelManager.CurrentLevel));
            HideRestartGameConfirmationCanvas();
            ResumeGame();
        }

        private void OnLoad(SaveData saveData)
        {
            if (saveData != null)
            {
                lolAdapter.SaveData = saveData;
            }
            else
            {
                lolAdapter.SaveData = new SaveData();
            }
            levelManager.CurrentLevelIndex = lolAdapter.SaveData.currentLevel;
            lolAdapter.SetSaveData(levelManager.CurrentLevelIndex);
            lolAdapter.SavePlayerProgress();
        }

        private void EndGame()
        {
            lolAdapter.Endgame();
        }

        public void LevelIsFinished()
        {
            StartCoroutine(LevelFinishedAnimation());
        }

        private IEnumerator LevelFinishedAnimation()
        {
            HideExampleCanvasAndShapesTable();
            levelManager.StartLevelAnimation();
            yield return new WaitForSeconds(levelManager.AnimationManager.GetAnimationDuration());
            levelManager.ResetCameraPositionAndSize();
            lolAdapter.IncreaseProgress();
            lolAdapter.SavePlayerProgress();
            if(levelManager.IsLastLevel())
            {
                ShowEndGameCanvas();
                EndGame();
            }
            else
            {
                ShowCongratulationCanvas();
            }
        }

        private void HideExampleCanvasAndShapesTable()
        {
            exampleCanvas.SetActive(false);
            shapesTable.SetActive(false);
        }
        private void ShowExampleCanvasAndShapesTable()
        {
            exampleCanvas.SetActive(true);
            shapesTable.SetActive(true);
        }

        public void GoNextLevel()
        {
            levelManager.LoadNextLevel();
            lolAdapter.SetSaveData(levelManager.CurrentLevelIndex);
            lolAdapter.SavePlayerProgress();
            lolAdapter.SaveGame();
            HideCongratulationCanvas();
            ShowExampleCanvasAndShapesTable();
        }

        public void PauseGame()
        {
            player.DisablePlayerInput();
            audioManager.PauseMusic();
            ShowPauseGameCanvas();
        }
        public void ResumeGame()
        {
            player.EnablePlayerInput();
            audioManager.ResumeMusic();
            HidePauseGameCanvas();
        }

        public void ShowCongratulationCanvas()
        {
            congratulationsCanvas.SetActive(true);
            //textManager.CancelText();
            textManager.SpeakText("congrats");
        }
        public void HideCongratulationCanvas()
        {
            congratulationsCanvas.SetActive(false);
        }
        public void ShowEndGameCanvas()
        {
            endgameCanvas.SetActive(true);
        }
        public void HideEndGameCanvas()
        {
            endgameCanvas.SetActive(false);
        }
        public void ShowPauseGameCanvas()
        {
            pauseCanvas.SetActive(true);
            //textManager.CancelText();
            textManager.SpeakText("paused");
        }
        public void HidePauseGameCanvas()
        {
            pauseCanvas.SetActive(false);
        }
        public void ShowTutorialCanvas()
        {
            tutorialCanvas.SetActive(true);
        }
        public void HideTutorialCanvas()
        {
            tutorialCanvas.SetActive(false);
        }
        public void ShowOptionsMenuCanvas()
        {
            optionsMenuCanvas.SetActive(true);
        }
        public void HideOptionsMenuCanvas()
        {
            optionsMenuCanvas.SetActive(false);
        }

        public void ShowRestartGameConfirmationCanvas()
        {
            restartGameConfirmationCanvas.SetActive(true);
            //textManager.CancelText();
            textManager.SpeakText("restartgame");
        }
        public void HideRestartGameConfirmationCanvas()
        {
            restartGameConfirmationCanvas.SetActive(false);
        }
    }
}
