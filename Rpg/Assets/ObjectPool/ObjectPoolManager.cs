using UnityEngine;
using UnityEngine.Events;
using Rpg.Character;
using UnityEngine.Audio;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    private FisherYatesRandom randomSource = new FisherYatesRandom();
    private CustomObjectPool objectPool = new CustomObjectPool();

    /// <summary>
    ///  hit 파티클 풀링
    /// </summary>
    /// <param name="particle"></param>
    /// <param name="hitEffectInfo"></param>
    public void TriggerHitParticlePooling(GameObject particle, HittEffectInfo hitEffectInfo, Transform parent)
    {
        objectPool.Register(particle, parent);
        GameObject defaultHitEffect = objectPool.GetInstance();
        defaultHitEffect.transform.position = hitEffectInfo.position;
        defaultHitEffect.transform.rotation = hitEffectInfo.rotation;
        StartCoroutine(objectPool.UnUseInsert(defaultHitEffect, 1));
    }
    /// <summary>
    /// 발걸음 소리 풀링
    /// </summary>
    /// <param name="audioSource"></param>
    /// <param name="footStepObject"></param>
    /// <param name="audioMixerGroup"></param>
    public void TriggerStepSound(GameObject audioSource, FootStepObject footStepObject, AudioMixerGroup audioMixerGroup, List<AudioClip> audioClips)
    {
        GameObject audioObject = null;

        if (audioSource != null)
        {
            objectPool.Register(audioSource, footStepObject.sender);
            audioObject = objectPool.GetInstance();
            audioObject.transform.position = footStepObject.sender.position;
            audioObject.transform.rotation = Quaternion.identity;
        }

        var source = audioObject.GetComponent<AudioSurfaceControl>();
        if (audioMixerGroup != null)
        {
            source.outputAudioMixerGroup = audioMixerGroup;
        }
        int index = randomSource.Next(audioClips.Count);
        source.PlayOneShot(audioClips[index], objectPool);
    }
    /// <summary>
    /// 스모크 on
    /// </summary>
    /// <param name="smoke"></param>
    /// <param name="footStepObject"></param>
    public void TriggerSmokePaticle(GameObject smoke, FootStepObject footStepObject)
    {
        GameObject stepSmoke = null;
        if (smoke)
        {
            objectPool.Register(smoke, footStepObject.sender);
            stepSmoke = objectPool.GetInstance();
            stepSmoke.transform.position = footStepObject.sender.position;
            stepSmoke.transform.rotation = footStepObject.sender.rotation;
            StartCoroutine(objectPool.UnUseInsert(stepSmoke, 0.5f));
        }
    }

    public void TriggerStepMarkPaticle(Transform markPoint , GameObject stepMark, FootStepObject footStep , LayerMask stepLayer)
    {
        RaycastHit hit;
        if (Physics.Raycast(markPoint.position + new Vector3(0, 0.1f, 0), -footStep.sender.up, out hit, 1f, stepLayer))
        {
            var angle = Quaternion.FromToRotation(footStep.sender.up, hit.normal);
            if (stepMark != null)
            {
                objectPool.Register(stepMark, stepMark.transform.parent);
                var step = objectPool.GetInstance();
                step.transform.position = hit.point;
                step.transform.rotation = angle * footStep.sender.rotation;
                StartCoroutine(objectPool.UnUseInsert(step , 3f));
            }
        }
    }
}

