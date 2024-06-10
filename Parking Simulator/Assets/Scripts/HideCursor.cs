using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Set Cursor to not be visible
        UnityEngine.Cursor.visible = false;
        // Lock the cursor to the center of the screen
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the cursor stays invisible
        if (UnityEngine.Cursor.visible)
        {
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
