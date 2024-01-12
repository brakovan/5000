using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;



public class TimelineManager : MonoBehaviour
{
    
    [SerializeField]
    private Transform fairy, player;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    CameraController cameraController;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    public void OnCutsceneStart()
    {
        playerController.isMove = false;
        cameraController.target = fairy;
        camera.orthographicSize = 3f;
    }

    public void OnCutsceneEnd()
    {
        cameraController.target = player;
        camera.orthographicSize = 5f;
        cameraController.target = player;
    }

}
