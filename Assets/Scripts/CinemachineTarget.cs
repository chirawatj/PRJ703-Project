using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineTarget : MonoBehaviour
{
    private CinemachineVirtualCamera cmCam;

    void Start()
    {
        // Get the virtual camera component attached to this GameObject (CmCam)
        cmCam = GetComponent<CinemachineVirtualCamera>();

        // Find the player by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Set the Follow target if the player is found
        if (player != null && cmCam != null)
        {
            cmCam.Follow = player.transform;
        }
    }
}
