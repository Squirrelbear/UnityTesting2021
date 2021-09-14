using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WalkOnButtonBehaviour : MonoBehaviour
{
    public event Action OnButtonEnteredEvent;
    public event Action OnButtonExitedEvent;

    private Vector3 defaultPosition;
    private Vector3 fullDownPosition;
    public bool ButtonDown { get; private set; }
    public float movesDownYUnits;

    private void Awake()
    {
        ButtonDown = false;
        defaultPosition = transform.position;
        fullDownPosition = new Vector3(defaultPosition.x, defaultPosition.y - movesDownYUnits, defaultPosition.z);
    }

    private void OnEnable()
    {
        OnButtonEnteredEvent += handleBeginEnter;
        OnButtonExitedEvent += handleBeginExit;
    }

    private void OnDisable()
    {
        OnButtonEnteredEvent -= handleBeginEnter;
        OnButtonExitedEvent -= handleBeginExit;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("ON");
            OnButtonEnteredEvent?.Invoke();
        }    
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("OFF");
            OnButtonExitedEvent?.Invoke();
        }
    }

    private void handleBeginEnter()
    {
        ButtonDown = true;
        CancelInvoke();
        StartCoroutine(moveToPosition(transform, fullDownPosition, 0.2f));
    }

    private void handleBeginExit()
    {
        ButtonDown = false;
        CancelInvoke();
        StartCoroutine(moveToPosition(transform, defaultPosition, 0.2f));
    }

    // Based on code from: https://answers.unity.com/questions/296347/move-transform-to-target-in-x-seconds.html
    private IEnumerator moveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
