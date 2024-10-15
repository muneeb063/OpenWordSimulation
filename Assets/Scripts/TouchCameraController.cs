using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchCameraController : MonoBehaviour
{
    public float swipeSensitivity = 0.1f; // Sensitivity of swipe rotation
    public Transform playerCamera; // Reference to the player's camera (FPS view)

    private Vector2 startTouchPosition, endTouchPosition;
    private bool isSwiping = false;

    void Update()
    {
        // Ensure the device has a touchscreen
        if (Touchscreen.current != null)
        {
            // Check if the primary touch is pressed (active swipe)
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

                if (!isSwiping)
                {
                    startTouchPosition = touchPosition;
                    isSwiping = true;
                }
                else
                {
                    endTouchPosition = touchPosition;
                }
            }
            else if (isSwiping) // When swipe is released
            {
                // Handle the swipe logic
                HandleSwipe();
                isSwiping = false;
            }
        }
    }

    // Rotate the camera based on swipe direction
    void HandleSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe detection
        {
            float horizontalSwipe = swipeDelta.x * swipeSensitivity;

            // Rotate the player camera based on swipe
            playerCamera.rotation = Quaternion.Euler(0f, playerCamera.rotation.eulerAngles.y + horizontalSwipe, 0f);
        }
    }
}
