using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

/*
 * Tegen de cube muur oplopen --> triggered een geluid
 * Springen + geluid bij springen
 * Locatie based --> music / soundscape
 * over op werken met velocity en dan voetstappen --> randomized sound container, zodat we verschillende samples --> snelheid
 */




public class PlayerController : MonoBehaviour
{
    public Transform head;
    public Camera camera;
    public float mouseSentivity = 0.5f;
    public float speed = 0;
    public float accelerationIntensity = 1.0f;
    
    public CharacterAudio characterAudio;
    
    private Rigidbody rb;
    private Vector2 moveInput = new Vector2(0, 0);
    private Vector2 lookInput = new Vector2(0, 0);
    Vector3 moveAxis;
    private float cameraVerticalAngle = 0;
    
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // update horizontal and vertical view direction based on lookInput
        UpdateViewDirection();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    /*
     * Update - OR use FixedUpdate
     * Update is called once per frame
     * FixedUpdate - when using rigidBody and physics, not implemented currently
     * however, physics in Updatae is not a problem if you multiply with Time.deltaTime
     * (In FixedUpdate Time.deltaTime is a constant, hence the 'fixed' update, so not necessary there)
     */

    void UpdateViewDirection()
    {
        // horizontal camera rotation - based on look input, around local Y axis
        transform.Rotate(new Vector3(0f, lookInput.x * mouseSentivity, 0),Space.Self);
        
        // vertical camera rotation - based on look input, around local Y axis
        {
            // subtract (reversed) vertical look input to vertical camera angle
            cameraVerticalAngle -= lookInput.y * mouseSentivity;

            // clamp camera to min and max angle
            cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -45f, 45f);

            // vertical angle as a local rotation
            camera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
        }
    }
    
    
    void HandleMovement()
    {
        // NOTE 2: since we are working with RB and physics,
        // this method needs to be called by FixedUpdate instead of Update.
        
        // transform the moveInput to worldspace so it is directed according to player's direction
        Vector3 worldspaceMoveInput = RetrieveWorldspaceMoveInput();

        // calculate new velocity 
        Vector3 targetVelocity = worldspaceMoveInput * speed;
        Vector3 playerVelocity = rb.linearVelocity;

#if TRUE
        // by applying linear interpolation with Lerp each frame --> exponentional curve
        playerVelocity = Vector3.Lerp(playerVelocity, targetVelocity, Time.fixedDeltaTime * accelerationIntensity);
#else
        // linear progression by maxDistanceDelta, third parameter of MoveTowards
        playerVelocity = Vector3.MoveTowards(playerVelocity, targetVelocity, Time.fixedDeltaTime * accelerationIntensity);
        // rb.MovePosition(rb.position + (worldspaceMoveInput * speed * Time.fixedDeltaTime));
#endif
        
        // move rigidbody
        rb.linearVelocity = playerVelocity;
    }

    
    
    Vector3 RetrieveWorldspaceMoveInput()
    {
        // NOTE: moveInput is a Vector2 representing the 2d input of
        // - the wasd or arrow buttons,
        // - the movement joystick 
        moveAxis = new Vector3(moveInput.x, 0, moveInput.y);
        moveAxis = Vector3.ClampMagnitude(moveAxis, 1);
        // only consider camera’s horizontal rotation (yaw)
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();
        
        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();
        
        Vector3 forwardMovement = forward * moveAxis.z;
        
        return forwardMovement + right * moveAxis.x;
    }

    void OnMove (InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
        
        // TODO - constrain moveInput to a maximum magnitude of 1
        // moveInput = Vector3.ClampMagnitude(moveInput, 1);
    }

    void OnLook(InputValue lookValue)
    {
        lookInput = lookValue.Get<Vector2>();
    }
    
    // TODO - OnSprint, OnInteract, OnJump, OnAttack, ...  

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            CollisionWithWall();
            characterAudio.PlayWallCollisionSound();
        }
    }
    void CollisionWithWall()
    {
        print("BOOM!");
    }
    
}
