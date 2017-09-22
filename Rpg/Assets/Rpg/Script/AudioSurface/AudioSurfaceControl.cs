using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

namespace Rpg.Character
{
    [RequireComponent(typeof(AudioSource))]
    class AudioSurfaceControl : MonoBehaviour
    {
        AudioSource source;
        bool isWorking;

        public void PlayOneShot(AudioClip clip)
        {
            if (!source)
                source = GetComponent<AudioSource>();

            source.PlayOneShot(clip);
            isWorking = true;
        }
        void Update()
        {
            if (isWorking && !source.isPlaying)           
                Destroy(gameObject);           
        }
        public AudioMixerGroup outputAudioMixerGroup
        {
            set
            {
                if (!source)
                    source = GetComponent<AudioSource>();

                source.outputAudioMixerGroup = value;
            }
        }
    }
}
