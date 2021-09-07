using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBehaviour : MonoBehaviour
{
    [SerializeField]
    private WalkOnButtonBehaviour linkedButton;
    [SerializeField]
    private float rollOnSpeed;
    [SerializeField]
    private float rollOffSpeed;

    [SerializeField]
    private float rollRotationMin;
    [SerializeField]
    private float rollRotationMax;

    [SerializeField]
    private float rollCurrent;
    [SerializeField]
    private bool isRolling;
    [SerializeField]
    private bool isRollingOn;

    [SerializeField]
    private bool isFrozen;

    public float getProgressPercent()
    {
        return (rollCurrent - rollRotationMin) / (rollRotationMax - rollRotationMin);
    }

    public void toggleFreeze()
    {
        isFrozen = !isFrozen;
    }

    private void Update()
    {
        /*if(!isRolling)
        {
            return;
        }*/

        if(isFrozen)
        {
            return;
        }

        if (linkedButton.ButtonDown)
        {
            rollCurrent += rollOnSpeed * Time.deltaTime;
            if(rollCurrent >= rollRotationMax)
            {
                isRolling = false;
                rollCurrent = rollRotationMax;
            }
        } 
        else
        {
            rollCurrent -= rollOffSpeed * Time.deltaTime;
            if (rollCurrent <= rollRotationMin)
            {
                isRolling = false;
                rollCurrent = rollRotationMin;
            }
        }

        transform.eulerAngles = new Vector3(0, 0, rollCurrent);

        //transform.rotation = Quaternion.AngleAxis(rollCurrent % 360, Vector3.up);
        //Vector3 currentRotation = transform.rotation.eulerAngles;
        //currentRotation.x = rollCurrent % 360;
        //Debug.Log(currentRotation);
        //transform.eulerAngles = currentRotation;
    }

    private void OnEnable()
    {
        if(linkedButton == null)
        {
            Debug.LogWarning("No Button Linked to RollerBehaviour!");
            return;
        }

        linkedButton.OnButtonEnteredEvent += handleStartRollingOn;
        linkedButton.OnButtonEnteredEvent += handleStartRollingOff;

        isRolling = false;
        isRollingOn = false;
        rollCurrent = rollRotationMin;
        isFrozen = false;
    }

    private void OnDisable()
    {
        if (linkedButton == null)
        {
            Debug.LogWarning("No Button Linked to RollerBehaviour!");
            return;
        }

        linkedButton.OnButtonEnteredEvent -= handleStartRollingOn;
        linkedButton.OnButtonEnteredEvent -= handleStartRollingOff;
    }

    private void handleStartRollingOn()
    {
        isRolling = true;
        isRollingOn = true;
    }

    private void handleStartRollingOff()
    {
        isRolling = true;
        isRollingOn = false;
    }
}
