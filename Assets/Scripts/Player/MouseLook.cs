using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxis axes = RotationAxis.MouseXAndY;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float minimumX = -360f;
    public float maximumX = 360f;

    public float minimumY = -60f;
    public float maximumY = 60f;

    public float rotationY = 0f;

    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
        
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            Vector2 mouseDelta = context.action.ReadValue<Vector2>();

            if (axes == RotationAxis.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + mouseDelta.x * sensitivityX;

                rotationY += mouseDelta.y * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxis.MouseX)
            {
                transform.Rotate(0, mouseDelta.x * sensitivityX, 0);
            }
            else
            {
                rotationY += mouseDelta.y * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
