using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Rpg.Character
{
    public class AudioSurface : ScriptableObject
    {
        public AudioSource audioSource;
        public AudioMixerGroup audioMixerGroup;                
        public List<string> TextureOrMaterialNames;            
        public List<AudioClip> audioClips;                     
        public GameObject particleObject;                                                                                            
        private FisherYatesRandom randomSource = new FisherYatesRandom();
         
        public AudioSurface()
        {
            audioClips = new List<AudioClip>();
            TextureOrMaterialNames = new List<string>();
        }

        public void PlayRandomClip(FootStepObject footStepObject)
        {
            if (audioClips == null || audioClips.Count == 0)
                return;

            if (randomSource == null)
                randomSource = new FisherYatesRandom();

            GameObject audioObject = null;
            if (audioSource != null)
            {
                audioObject = Instantiate(audioSource.gameObject, footStepObject.sender.position, Quaternion.identity) as GameObject;
            }
            else
            {
                audioObject = new GameObject("audioObject");
                audioObject.transform.position = footStepObject.sender.position;
            }

            var source = audioObject.AddComponent<AudioSurfaceControl>();
            if (audioMixerGroup != null)
            {
                source.outputAudioMixerGroup = audioMixerGroup;
            }
            int index = randomSource.Next(audioClips.Count);
            if (particleObject)
            {
                var particle = Instantiate(particleObject, footStepObject.sender.position, footStepObject.sender.rotation) as GameObject;
                particle.SendMessage("StepMark", footStepObject, SendMessageOptions.DontRequireReceiver);
            }
            source.PlayOneShot(audioClips[index]);
        }
    }
}
