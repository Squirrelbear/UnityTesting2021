using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGrowBehaviour : MonoBehaviour
{
    [SerializeField]
    private RollerBehaviour linkedRoller;

    [SerializeField]
    private float minScale;
    [SerializeField]
    private float maxScale;

    [SerializeField]
    private float timeToFreeze;
    [SerializeField]
    private float timeToUnFreeze;

    private Material[] allSubMaterials;

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = new Vector3(minScale, minScale, minScale);
        linkedRoller.OnFreezeEvent += handleOnFreezeStart;
        linkedRoller.OnUnFreezeEvent += handleOnUnFreezeStart;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        allSubMaterials = new Material[renderers.Length];
        for(int i = 0; i < renderers.Length; i++)
        {
            allSubMaterials[i] = renderers[i].material;
        }
    }

    void OnDisable()
    {
        transform.localScale = new Vector3(minScale, minScale, minScale);
        linkedRoller.OnFreezeEvent -= handleOnFreezeStart;
        linkedRoller.OnUnFreezeEvent -= handleOnUnFreezeStart;
    }

    private void handleOnFreezeStart()
    {
        StopAllCoroutines();
        StartCoroutine(scaleOverTime(transform, new Vector3(maxScale, maxScale, maxScale), timeToFreeze));
    }

    private void handleOnUnFreezeStart()
    {
        StopAllCoroutines();
        StartCoroutine(fadeOverTime(timeToUnFreeze));
    }

    private IEnumerator scaleOverTime(Transform transform, Vector3 goalScale, float timeToChange)
    {
        var currentScale = transform.localScale;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToChange;
            transform.localScale = Vector3.Lerp(currentScale, goalScale, t);
            yield return null;
        }
    }

    private IEnumerator fadeOverTime(float timeToChange)
    {
        Color c = allSubMaterials[0].color;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToChange;
            c.a = 1-t;
            foreach (Material m in allSubMaterials)
            {
                m.color = c;
            }
            yield return null;
        }
        transform.localScale = new Vector3(minScale, minScale, minScale);
        c.a = 1;
        foreach (Material m in allSubMaterials)
        {
            m.color = c;
        }
    }
}
