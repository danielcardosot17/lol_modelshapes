using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalangoGames
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<Sound> soundEffcts;
        [SerializeField] private List<Sound> musics;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TMP_Text volumeText;
        [SerializeField][Range(0.0f, 10f)] private float stickAndScrewTimer = 0;
        private AudioSource audioSource;
        private List<AudioSource> sfxSources;
        private float sfxTimer = 0;

        private void Awake() {
            sfxSources = new List<AudioSource>();
            audioSource = GetComponent<AudioSource>();
            StopMusic();
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        public void PlayMusic(string musicName)
        {
            if(musics.Count == 0) return;
            var sound = musics.Find(sound => sound.name == musicName);
            audioSource.clip = sound.clip;
            audioSource.priority = sound.priority;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.loop = sound.loop;
            audioSource.Play();
        }

        public void PlaySFX(string soundName, Vector3 position = new Vector3())
        {
            if(soundEffcts.Count == 0) return;
            var sound = soundEffcts.Find(sound => sound.name == soundName);
            var obj = new GameObject(name: soundName, typeof(AudioSource));
            obj.transform.position = position;
            var source = obj.GetComponent<AudioSource>();

            source.clip = sound.clip;
            source.priority = sound.priority;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            source.Play();
            sfxSources.Add(source);
            if (!source.loop)
            {
                StartCoroutine(DoAfterTimeCoroutine(source.clip.length / source.pitch, () =>
                {
                    sfxSources.Remove(source);
                    Destroy(source.gameObject);
                }));
                //Destroy(source.gameObject, source.clip.length/source.pitch);
            }
        }

        public void PauseMusic()
        {
            audioSource.Pause();
        }

        public void ResumeMusic()
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        public void PauseAllSFX()
        {
            foreach(var sfx in sfxSources)
            {
                sfx.Pause();
            }
        }

        public void PlayButtonSFX()
        {
            PlaySFX("button");
        }

        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;
        }
        public void ChangeVolumeText()
        {
            volumeText.text = (volumeSlider.value * 100).ToString("N0");
        }

        public static IEnumerator DoAfterTimeCoroutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        public void PlayStickOrClaySFX(string stickOrClay)
        {
            if (sfxTimer <= 0)
            {
                PlaySFX(stickOrClay);
                sfxTimer = stickAndScrewTimer;
            }
        }

        private void Update()
        {
            if(sfxTimer > 0)
            {
                sfxTimer -= Time.deltaTime;
            }
        }
    }
}
