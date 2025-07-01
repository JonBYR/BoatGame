using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
public class SwitchCameras : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera backCamera;
    [SerializeField] InputActionReference cameraButton;
    private CinemachineVirtualCamera currentCamera;
    // Start is called before the first frame update
    void Start()
    {
        currentCamera = mainCamera;
        ChangePriorities(currentCamera);
    }

    private void OnEnable()
    {
        cameraButton.action.started += ChangeCamera;
    }
    private void OnDisable()
    {
        cameraButton.action.started -= ChangeCamera;
    }
    void ChangeCamera(InputAction.CallbackContext context)
    {
        if (mainCamera == currentCamera) currentCamera = backCamera;
        else currentCamera = mainCamera;
        ChangePriorities(currentCamera);
    }
    void ChangePriorities(CinemachineVirtualCamera cam)
    {
        if (mainCamera == currentCamera)
        {
            mainCamera.Priority = 20;
            backCamera.Priority = 5;
        }
        else
        {
            mainCamera.Priority = 5;
            backCamera.Priority = 20;
        }
    }
}
