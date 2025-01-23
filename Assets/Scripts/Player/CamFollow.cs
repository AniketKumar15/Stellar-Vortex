using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject playerContainer; 
    public float smoothSpeed = 0.125f; // Speed of the camera smoothing
    public Vector3 offset; // Offset from the player position

    private Transform activePlayer; // The player's transform

    private void FixedUpdate()
    {
        if (activePlayer == null)
        {
            FindActivePlayer(); // Check for the active player if not already found
            if (activePlayer == null) return; // Exit if no active player is found
        }

        // Desired camera position based on the player's position and offset
        Vector3 desiredPosition = activePlayer.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
        
    }

    private void FindActivePlayer()
    {
        // Loop through all children in the player container
        foreach (Transform child in playerContainer.transform)
        {
            if (child.gameObject.activeSelf)
            {
                activePlayer = child; // Set the active player
                break;
            }
        }
    }
}
