using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeManager : MonoBehaviour
{
    public static AxeManager instance;

    [SerializeField]
    private GameObject axeInHand;

    private Animator animator;
    private Animation throwAnimation;

    [SerializeField]
    public bool isEquipped { get; private set; }
    [SerializeField]
    public bool canBeRecalled { get; private set; }

    private ThrowAxeTargetBehaviour currentTarget;

    private void Awake()
    {
        instance = this;
        GetComponentInChildren<Renderer>().enabled = false;
    }

    public void requestThrowAtTarget(Vector3 exactPosition, ThrowAxeTargetBehaviour targetObjectBehaviour)
    {
        if(!isEquipped)
        {
            return;
        }
        transform.position = axeInHand.transform.position;
        transform.rotation = axeInHand.transform.rotation;
        axeInHand.GetComponentInChildren<Renderer>().enabled = false;
        GetComponentInChildren<Renderer>().enabled = true;
        isEquipped = false;
        currentTarget = targetObjectBehaviour;
        StartCoroutine(throwAtTarget(transform, exactPosition, Vector3.Distance(transform.position, exactPosition) / 10));
    }

    public void recallAxe()
    {
        if(!canBeRecalled)
        {
            return;
        }

        currentTarget.handleAxeLeave();
        currentTarget = null;
        isEquipped = true;
        canBeRecalled = false;
        transform.position = axeInHand.transform.position;
        transform.rotation = axeInHand.transform.rotation;
        axeInHand.GetComponentInChildren<Renderer>().enabled = true;
        GetComponentInChildren<Renderer>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isEquipped = true;
        canBeRecalled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            recallAxe();
        }
    }

    private IEnumerator throwAtTarget(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            transform.Rotate(new Vector3(0,0,360 * Time.deltaTime));
            yield return null;
        }

        canBeRecalled = true;
        currentTarget.handleAxeHit();
    }
}
