using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VT.Audio
{
    public static class AudioManager
    {
        private static GameObject s_oneShotGameObject;
        private static AudioSource s_oneShotAudioSource;
        
        /// <summary>
        ///   <para>Plays a sound as an one shot.</para>
        /// </summary>
        /// <param name="audio">The audio object to be played.</param>
        public static void PlaySound(AudioObject audio)
        {
            if (!audio.CanPlay())
                return;
            
            if (!s_oneShotGameObject)
            {
                s_oneShotGameObject = new GameObject("One Shot Sound");
                s_oneShotAudioSource = s_oneShotGameObject.AddComponent<AudioSource>();
            }

            audio.SetAudioSource(ref s_oneShotAudioSource);
            // _oneShotAudioSource.Play();
            
            s_oneShotAudioSource.PlayOneShot(audio.GetClip(), audio.GetVolume());
        }

        /// <summary>
        ///   <para>Plays a 3D sound on a given position.</para>
        /// </summary>
        /// <param name="audio">The audio object to be played.</param>
        /// <param name="position">The 3D position where the sound will be played.</param>
        public static void PlaySound(AudioObject audio, Vector3 position)
        {
            //todo: make this function use object pooling instead of instantiation
            if (audio == null)
            {
                Debug.LogWarning("No audio provided");
                return;
            }

            if (!audio.CanPlay())
                return;

            var go = new GameObject($"{audio.name} SFX");
            var goSource = go.AddComponent<AudioSource>();
            
            go.transform.position = position;

            audio.SetAudioSource(ref goSource);
            
            goSource.maxDistance = 100f;
            goSource.spatialBlend = 1;
            goSource.rolloffMode = AudioRolloffMode.Linear;
            goSource.dopplerLevel = 0;
            
            goSource.Play();
            
            Object.Destroy(go, goSource.clip.length);
            
        }

        /// <summary>
        ///   <para>Plays a 2D sound on a given position.</para>
        /// </summary>
        /// <param name="audio">The audio object to be played.</param>
        /// <param name="position">The 2D position where the sound will be played.</param>
        public static void PlaySound(AudioObject audio, Vector2 position)
        {
            var pos = new Vector3(position.x, position.y, 0);
            PlaySound(audio, pos);
        }
    }
}