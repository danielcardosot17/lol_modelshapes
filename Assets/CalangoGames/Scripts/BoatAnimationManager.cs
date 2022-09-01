using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class BoatAnimationManager : MonoBehaviour, IAnimationManager
    {
        public float animationDuration;
        public GameObject boat;
        public float nearSpeed;
        public float boatSpeed;
        public float farSpeed;
        public Material nearMaterial;
        public Material boatMaterial;
        public Material farMaterial;
        public Vector3 finalCameraPosition;
        public float finalCameraSize;
        public float cameraMoveDuration;


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
            boat.SetActive(true);
        }

        public void StartBackgroundAnimation()
        {
            isBackgroundMoving = true;
        }

        float nearOffset = 0;
        float boatOffset = 0;
        float farOffset = 0;
        private void Update()
        {
            if(isBackgroundMoving)
            {
                nearOffset += Time.deltaTime * nearSpeed;
                if (nearOffset >= 1) nearOffset = 0;
                nearMaterial.mainTextureOffset = new Vector2(nearOffset, 0);

                boatOffset += Time.deltaTime * boatSpeed;
                if (boatOffset >= 1) boatOffset = 0;
                boatMaterial.mainTextureOffset = new Vector2(boatOffset, 0);

                farOffset += Time.deltaTime * farSpeed;
                if (farOffset >= 1) farOffset = 0;
                farMaterial.mainTextureOffset = new Vector2(farOffset, 0);

            }
        }

        public void StartSFX()
        {
            audioManager.PlaySFX("Boat_Sea");
            audioManager.PlaySFX("Boat_Bells");
        }

        public void StartShapeAnimation()
        {
            boat.GetComponent<Animator>().enabled = true;
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
