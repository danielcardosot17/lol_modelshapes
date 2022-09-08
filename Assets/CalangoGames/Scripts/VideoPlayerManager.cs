using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace CalangoGames
{
    public class VideoPlayerManager : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        private string url;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();

#if UNITY_EDITOR
            //url = Application.dataPath + "/CalangoGames/Art/Textures/Tutorial/tutorial.mp4";
            url = "file://" + Application.streamingAssetsPath + "/" + "tutorial.mp4";
#else
            url = Application.streamingAssetsPath + "/" + "tutorial.mp4";
#endif
        }

        public void PlayVideo()
        {
            videoPlayer.url = url;
            Debug.Log(url);
            StartCoroutine(PlayTutorial());
        }

        public IEnumerator PlayTutorial()
        {
            videoPlayer.Prepare();

            WaitForSeconds waitTime = new WaitForSeconds(3);
            while (!videoPlayer.isPrepared)
            {
                Debug.Log("Preparing Video");
                //Prepare/Wait for 5 sceonds only
                yield return waitTime;
                //Break out of the while loop after 5 seconds wait
                break;
            }
            videoPlayer.Play();
        }
    }
}
