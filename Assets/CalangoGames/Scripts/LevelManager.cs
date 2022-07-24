using System.Collections;
using System.Collections.Generic;
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
        private bool isLoaded = false;

        public bool IsLoaded { get => isLoaded; set => isLoaded = value; }
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private Animator transitionAnimator;
        [SerializeField] private float transitionTime = 1f;

        private bool isLoading = false;
        private int currentLevelIndex = 0;
        private bool isEnd = false;

        public bool IsEnd { get => isEnd; }

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
    }
}
