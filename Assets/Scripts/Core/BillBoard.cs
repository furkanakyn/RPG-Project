using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;
    private void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.forward);
        }
        else
        {
            Debug.LogWarning("Camera Tranform is not assigned in the BillBoard script on " + gameObject.name);
        }
    }
}
