using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace EnglishKids.ChallengeGame
{
    public class SoundManager : MonoBehaviour
    {
        public AudioMixerGroup audioMixer;
        public Sound[] sounds;

        private void Start()
        {
            foreach (Sound sound in sounds)
            {
                sound.audioSource = gameObject.AddComponent<AudioSource>();
                sound.audioSource.clip = sound.audioClip;

                sound.audioSource.volume = sound.volume;
                sound.audioSource.playOnAwake = false;

                sound.audioSource.outputAudioMixerGroup = audioMixer;

            }
        }

        public void Play(string name)
        {
            if (!isPlaying())
            {
                Array.Find(sounds, sound => sound.name == name).audioSource.Play();
            }
        }

        public void Stop(string name)
        {
            Array.Find(sounds, sound => sound.name == name).audioSource.Stop();
        }

        public bool isPlaying(string name)
        {
            return Array.Find(sounds, sound => sound.name == name).audioSource.isPlaying;
        }

        private bool isPlaying()
        {
            bool res = false;
            for (int i = 0; i < sounds.Length; i++)
                if (sounds[i].audioSource.isPlaying)
                    res = true;
            return res;
        }
    }
}