using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float sesitivity = 500f;
    float rotationX;
    float rotationY;

    public Transform cameraTransform;
    public CharacterController characterController;
    public float moveSpeed = 10f;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
        float mouseMoveX = Input.GetAxis("Mouse X");
        float mouseMoveY = Input.GetAxis("Mouse Y");

        rotationY += mouseMoveX * sesitivity * Time.deltaTime;
        rotationX += mouseMoveY * sesitivity * Time.deltaTime;

        if (rotationX > 85f)
            rotationX = 85f;
        if (rotationX < -85f)
            rotationX = -85f;

        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(h, 0, v);

        moveDirection = cameraTransform.TransformDirection(moveDirection);

        moveDirection *= moveSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}