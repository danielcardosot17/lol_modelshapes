using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class WindmillAnimationManager : MonoBehaviour, IAnimationManager
    {
        public float animationDuration;
        public GameObject background;
        public GameObject blade1;
        public GameObject blade2;
        public GameObject blade3;
        public GameObject blade4;
        public GameObject blade5;
        public float bladeSpeed;
        public Vector3 finalCameraPosition;
        public float finalCameraSize;
        public float cameraMoveDuration;
        public GameObject wind;


        private Camera mainCamera;
        private AudioManager audioManager;
        private bool isBackgroundMoving = false;

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
            blade1.SetActive(true);
            background.SetActive(true);
            wind.SetActive(true);
        }

        public void StartBackgroundAnimation()
        {
            isBackgroundMoving = true;
        }

        private void Update()
        {
            if (isBackgroundMoving)
            {
                blade1.transform.Rotate(Vector3.forward * bladeSpeed * Time.deltaTime);
                blade2.transform.Rotate(Vector3.forward * bladeSpeed * Time.deltaTime);
                blade3.transform.Rotate(Vector3.forward * bladeSpeed * Time.deltaTime);
                blade4.transform.Rotate(Vector3.forward * bladeSpeed * Time.deltaTime);
                blade5.transform.Rotate(Vector3.forward * bladeSpeed * Time.deltaTime);
            }
        }

        public void StartSFX()
        {
            audioManager.PlaySFX("Windmill_Wind");
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
