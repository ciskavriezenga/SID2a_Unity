using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform head;
    public Camera camera;
    
    private Vector3 movementVector = new Vector3 (0.0f, 0.0f, 0.0f);
    private Vector3 cameraRotation = new Vector3 (0.0f, 0.0f, 0.0f);
    public float speed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        movementVector = transform.rotation * movementVector;
        transform.position += movementVector * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMove (InputValue movementValue)
    {
        movementVector.x = movementValue.Get<Vector2>().x;
        // need to map y to z
        movementVector.z = movementValue.Get<Vector2>().y;
    }

    void OnLook(InputValue lookValue)
    {
        float mouseSentivity = 0.5f;
        Vector2 lookRotation = lookValue.Get<Vector2>() * mouseSentivity;
        
        // rotate the transform of the player to enable movement in horizontal looking direction
        transform.Rotate(0, lookRotation.x, 0);
        
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -30, 30);
        camera.transform.localRotation = Quaternion.Euler(cameraRotation.y, 0, 0);
    }
    
    // TODO - OnSprint

}
