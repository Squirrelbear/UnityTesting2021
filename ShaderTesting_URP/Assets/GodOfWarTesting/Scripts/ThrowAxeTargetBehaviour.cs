using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxeTargetBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, 100000))
        {
            AxeManager.instance.requestThrowAtTarget(hitInfo.point, this);
        }
    }

    public void handleAxeHit()
    {
        GetComponent<RollerBehaviour>().toggleFreeze();
    }

    public void handleAxeLeave()
    {
        GetComponent<RollerBehaviour>().toggleFreeze();
    }
}
