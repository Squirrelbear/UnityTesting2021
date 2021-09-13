using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector3 maxOffset;
    private Vector3 defaultPosition;
    private Vector3 maxOffsetPosition;

    [SerializeField]
    private RollerBehaviour rollerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = transform.position;
        maxOffsetPosition = defaultPosition + maxOffset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(defaultPosition, maxOffsetPosition, rollerBehaviour.getProgressPercent());
    }
}
