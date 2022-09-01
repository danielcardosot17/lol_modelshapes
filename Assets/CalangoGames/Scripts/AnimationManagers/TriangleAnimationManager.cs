using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class TriangleAnimationManager : MonoBehaviour, IAnimationManager
    {
        public float animationDuration;
        public GameObject triangle;
        private AudioManager audioManager;

        private void Awake()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        public float GetAnimationDuration()
        {
            return animationDuration;
        }

        public void MoveCamera()
        {

        }

        public void ShowFinishedShape()
        {
            triangle.SetActive(true);
        }

        public void StartBackgroundAnimation()
        {
        }

        public void StartSFX()
        {
            audioManager.PlaySFX("Success");
        }

        public void StartShapeAnimation()
        {
        }
    }
}
