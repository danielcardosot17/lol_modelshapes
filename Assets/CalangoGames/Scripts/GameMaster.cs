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

        private LevelManager levelManager;
        private LOLAdapter lolAdapter;
        private Player player;

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
            StartLevelAnimation();
            yield return new WaitForSeconds(1);
            SavePlayerProgress();
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

        }

        private void ShowEndGameCanvas()
        {

        }

        private void StartLevelAnimation()
        {

        }

        private void SavePlayerProgress()
        {

        }
    }
}
