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
        public Vector3 finalCameraPosition;
        private Camera mainCamera;
        public float cameraMoveDuration;

        private void Awake()
        {
            mainCamera = Camera.main;
            audioManager = FindObjectOfType<AudioManager>();
        }

        public float GetAnimationDuration()
        {
            return animationDuration;
        }

        public void MoveCamera()
        {
            StartCoroutine(LerpPosition(finalCameraPosition, cameraMoveDuration));
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
            triangle.GetComponent<Animator>().enabled = true;
        }


        IEnumerator LerpPosition(Vector3 targetPosition, float duration)
        {
            float time = 0;
            Vector3 startPosition = mainCamera.transform.position;
            while (time < duration)
            {
                mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            mainCamera.transform.position = targetPosition;
        }
    }
}
