using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VT.Audio
{
    [CreateAssetMenu(menuName = "Audio/Audio Object", fileName = "AudioObject")]
    public class AudioObject : ScriptableObject
    {
        [SerializeField] private AudioClip[] clips;

        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool loop;
        [Range(0, 1)] [SerializeField] private float volume = 1f;
        [Range(0, 1)] [SerializeField] private float volumeVariance;
        [Range(-3, 3)] [SerializeField] private float pitch = 1f;
        [Range(0, 3)] [SerializeField] private float pitchVariance;
        [SerializeField] private float playDelay = 0.1f;
        
        private float m_lastPlayedTime;

        private void OnEnable()
        {
            m_lastPlayedTime = 0;
        }

        public void SetAudioSource(ref AudioSource source)
        {
            if (clips == null || clips.Length == 0)
                Debug.LogError($"Audio Object {name} has no clips");

            if (clips != null && clips.Length > 0)
                source.clip = GetClip();

            source.playOnAwake = playOnAwake;
            source.loop = loop;
            source.volume = GetVolume();
            source.pitch = GetPitch();
        }

        public bool CanPlay()
        {
            if (m_lastPlayedTime + playDelay > Time.time)
                return false;

            m_lastPlayedTime = Time.time;
            return true;
        }

        public float GetVolume() => Mathf.Clamp01(volume + Random.Range(-volumeVariance, volumeVariance));

        public AudioClip GetClip() => clips.Length == 1 ? clips[0] : clips[Random.Range(0, clips.Length)];

        public float GetPitch() => Mathf.Clamp(pitch + Random.Range(-pitchVariance, pitchVariance), -3, 3);
    }
}