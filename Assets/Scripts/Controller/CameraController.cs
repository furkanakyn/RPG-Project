using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] Camera[] cutsceneCameras; 
    [SerializeField] float cutsceneDuration = 5f; 
    public PlayerController playerController;

    private void Start()
    {
        foreach (var camera in cutsceneCameras)
        {
            camera.gameObject.SetActive(false);
        }
    }

    public void StartCutscene()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        playerController.enabled = false;
        mainCamera.gameObject.SetActive(false);

       
        foreach (var camera in cutsceneCameras)
        {
            camera.gameObject.SetActive(true);
            yield return new WaitForSeconds(cutsceneDuration/ cutsceneCameras.Length);
            camera.gameObject.SetActive(false);
        }

    
        mainCamera.gameObject.SetActive(true);
        playerController.enabled = true;
    }
}
