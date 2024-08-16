using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] GameObject endPoint1; 
    [SerializeField] GameObject endPoint2;
    [SerializeField] CameraController cameraController;
    public GameController gameController;




    public void MakeDirectionVisible()
    {
        gameObject.SetActive(true);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            endPoint1.SetActive(true);
            endPoint2.SetActive(true);
            gameObject.SetActive(false);
            gameController.player.GetComponent<Mover>().Cancel();
            gameController.player.GetComponent<ActionScheduler>().CancelCurrentAction();
            if (cameraController != null)
            {
                cameraController.StartCutscene();
            }
            gameController.SpawnQueen();
        }
    }
}
