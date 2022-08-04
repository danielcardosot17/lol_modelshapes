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

        private LevelManager levelManager;
        private LOLAdapter lolAdapter;
        private Player player;
        private AudioManager audioManager;

        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
            lolAdapter = new LOLAdapter();
            player = FindObjectOfType<Player>();
        }
        // Start is called before the first frame update
        void Start()
        {
            lolAdapter.Initialize();
            lolAdapter.SetGameReady();
            StartCoroutine(lolAdapter.WaitForApproval());

            StartGame();
        }

        private void StartGame()
        {
            StartCoroutine(levelManager.LoadLevel(levelManager.CurrentLevel));
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
            levelManager.StartLevelAnimation();
            yield return new WaitForSeconds(1);
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

        private void ShowCongratulationCanvas()
        {
            congratulationsCanvas.SetActive(true);
        }
        private void HideCongratulationCanvas()
        {
            congratulationsCanvas.SetActive(false);
        }

        private void ShowEndGameCanvas()
        {
            endgameCanvas.SetActive(true);
        }
        private void HideEndGameCanvas()
        {
            endgameCanvas.SetActive(false);
        }

        public void PauseGame()
        {
            player.DisablePlayerInput();
            audioManager.PauseMusic();
        }

        public void ResumeGame()
        {
            player.EnablePlayerInput();
            audioManager.ResumeMusic();
        }

        public void ShowTutorial()
        {
            ShowTutorialCanvas();
        }

        private void ShowTutorialCanvas()
        {
            tutorialCanvas.SetActive(true);
        }
    }
}
