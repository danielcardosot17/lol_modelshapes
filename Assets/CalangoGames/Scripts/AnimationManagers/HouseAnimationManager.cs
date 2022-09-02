using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class HouseAnimationManager : MonoBehaviour, IAnimationManager
    {
        public float animationDuration;
        public GameObject house;
        private AudioManager audioManager;
        public Vector3 finalCameraPosition;
        private Camera mainCamera;
        public float cameraMoveDuration;
        public float finalCameraSize;

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
            StartCoroutine(LerpCameraSize(finalCameraSize, cameraMoveDuration));
        }

        public void ShowFinishedShape()
        {
            house.SetActive(true);
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
        IEnumerator LerpCameraSize(float endValue, float duration)
        {
            float time = 0;
            float startValue = 20;
            while (time < duration)
            {
                mainCamera.orthographicSize = Mathf.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            mainCamera.orthographicSize = endValue;
        }
    }
}
