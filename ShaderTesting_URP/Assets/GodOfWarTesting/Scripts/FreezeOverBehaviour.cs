using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOverBehaviour : MonoBehaviour
{
    [SerializeField]
    private RollerBehaviour linkedRoller;

    private Material material;

    [SerializeField]
    private float timeToFreeze;
    [SerializeField]
    private float timeToUnFreeze;

    // Start is called before the first frame update
    void OnEnable()
    {
        material = GetComponent<Renderer>().material;
        linkedRoller.OnFreezeEvent += handleOnFreezeStart;
        linkedRoller.OnUnFreezeEvent += handleOnUnFreezeStart;
    }

    void OnDisable()
    {
        linkedRoller.OnFreezeEvent -= handleOnFreezeStart;
        linkedRoller.OnUnFreezeEvent -= handleOnUnFreezeStart;
    }

    private void handleOnFreezeStart()
    {
        StopAllCoroutines();
        StartCoroutine(changeRevealState(transform, 1f, timeToFreeze));
    }

    private void handleOnUnFreezeStart()
    {
        StopAllCoroutines();
        StartCoroutine(changeRevealState(transform, 0f, timeToUnFreeze));
    }

    private IEnumerator changeRevealState(Transform transform, float goalValue, float timeToChange)
    {
        var revealAmount = material.GetFloat("_FreezeAmount");
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToChange;
            material.SetFloat("_FreezeAmount", Mathf.Lerp(revealAmount, goalValue, t));
            yield return null;
        }
    }
}
