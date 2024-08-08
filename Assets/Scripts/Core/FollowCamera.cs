using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform player; 
    

    public float rotationSpeed = 5.0f;


    void Update()
    {
        transform.position = player.position;

    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);

       
        Quaternion desiredRotation = Quaternion.LookRotation(player.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
