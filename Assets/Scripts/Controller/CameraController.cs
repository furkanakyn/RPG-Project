using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Camera[] cutsceneCameras; 
    [SerializeField] private float cutsceneDuration = 5f; 

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
       
        mainCamera.gameObject.SetActive(false);

       
        foreach (var camera in cutsceneCameras)
        {
            camera.gameObject.SetActive(true);
            yield return new WaitForSeconds(cutsceneDuration/ cutsceneCameras.Length);
            camera.gameObject.SetActive(false);
        }

    
        mainCamera.gameObject.SetActive(true);
    }
}
