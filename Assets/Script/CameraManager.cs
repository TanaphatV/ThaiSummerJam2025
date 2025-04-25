using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras; // Array of virtual cameras
    private bool isCam1Active = true; // Tracks which camera is active for toggling

    public void SwitchToCam(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= virtualCameras.Length)
        {
            Debug.LogError("Invalid camera index!");
            return;
        }

        // Set the priority of all cameras to 0
        foreach (var cam in virtualCameras)
        {
            cam.Priority = 0;
        }

        // Set the priority of the selected camera to a higher value
        virtualCameras[cameraIndex].Priority = 10;
    }

    public void Update()
    {  
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     SwitchToCam(0);
        // }

        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     SwitchToCam(1); // TPS
        // }

        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     SwitchToCam(2); // FPS
        // }

        // Toggle between Game View 1 & 2 when pressing V
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isCam1Active)
            {
                SwitchToCam(1); // Switch to Camera 2
            }
            else
            {
                SwitchToCam(2); // Switch to Camera 3
            }

            isCam1Active = !isCam1Active; // Toggle the state
        }
    }
}