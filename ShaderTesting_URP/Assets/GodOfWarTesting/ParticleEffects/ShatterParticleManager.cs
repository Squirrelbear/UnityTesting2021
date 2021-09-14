using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ShatterParticleManager : MonoBehaviour
{
    [SerializeField]
    private VisualEffect shatterEffect;

    [SerializeField]
    private AnimationCurve spawnRateOverTime;

    [SerializeField]
    private float maxSpawnRate;

    [SerializeField]
    private RollerBehaviour rollerBehaviour;

    private void OnEnable()
    {
        rollerBehaviour.OnUnFreezeEvent += handleUnFreezeEvent;
    }

    private void OnDisable()
    {
        rollerBehaviour.OnUnFreezeEvent -= handleUnFreezeEvent;
    }

    public void handleUnFreezeEvent()
    {
        StopAllCoroutines();
        StartCoroutine(applySpawnRateOverTime());
    }

    private IEnumerator applySpawnRateOverTime()
    {
        float t = 0f;
        while (t < spawnRateOverTime.length)
        {
            t += Time.deltaTime;
            shatterEffect.SetFloat("SpawnRate", spawnRateOverTime.Evaluate(t)*maxSpawnRate);
            yield return null;
        }
        shatterEffect.SetFloat("SpawnRate", 0);
    }
}
