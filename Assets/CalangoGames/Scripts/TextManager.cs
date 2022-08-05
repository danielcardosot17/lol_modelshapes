using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalangoGames
{
    public class TextManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text pausedText;
        [SerializeField] private TMP_Text restartGameText;
        //[SerializeField] private TMP_Text musicText;
        //[SerializeField] private TMP_Text sfxText;
        //[SerializeField] private TMP_Text tutorialText;
        //[SerializeField] private TMP_Text loadingText;
        [SerializeField] private TMP_Text congratsText;
        [SerializeField] private TMP_Text letsBuildText;
        //[SerializeField] private TMP_Text endgameText;
        [SerializeField] private TMP_Text levelNameText;

        public void UpdateGameText()
        {
            pausedText.text = GetText("paused", "Paused");
            restartGameText.text = GetText("restartgame", "Restart Game?");
            //musicText.text = GetText("music", "Music");
            //sfxText.text = GetText("sfx", "SFX");
            //tutorialText.text = GetText("tutorial", "How to Play");
            //loadingText.text = GetText("loading", "Loading...");
            congratsText.text = GetText("congrats", "Great!");
            letsBuildText.text = GetText("letsbuild", "Let's Build:");
            //endgameText.text = GetText("endgame", "Game Complete!");
        }
        public void UpdateLevelNameText(string name)
        {
            levelNameText.text = GetText(name.ToLower(), name);
        }

        string GetText(string key, string defaultValue)
        {
            string value = SharedState.LanguageDefs?[key];
            return value ?? defaultValue;
        }
    }
}
