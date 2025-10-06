using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Tegen de cube muur oplopen --> triggered een geluid
 * Springen + geluid bij springen
 *  addForce, isGrounded method
 * return Physics.Raycast(transform.position, Vector3.down)}
 * https://learn.unity.com/pathway/junior-programmer/unit/sound-and-effects/tutorial/lesson-3-1-jump-force-2
 * Locatie based --> music / soundscape
 * over op werken met velocity en dan voetstappen --> randomized sound container, zodat we verschillende samples --> snelheid
 */

// NOTE: player footsteps audio clips from freesound.org made by TechspiredMinds. Thanks! 



public class PlayerController : MonoBehaviour
{
    public Transform head;
    public Camera camera;
    public CharacterAudio characterAudio;
    
    public float mouseSentivity = 0.5f;
    public float speed = 0.0f;
    public float accelerationIntensity = 1.0f;
    public float jumpAmount = 35.0f;
    public float gravityScale = 10.0f;
    public float footstepDistance = 0.5f;
    
    
    private Rigidbody rb;
    private Vector2 moveInput = new Vector2(0, 0);
    Vector3 moveAxis;
    private Vector2 lookInput = new Vector2(0, 0);
    private float cameraVerticalAngle = 0;

    private Boolean isJumping = false;
    private Boolean startJump = false;

    private float footstepDistanceCounter = 0;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // update horizontal and vertical view direction based on lookInput 
        // TODO - using transform here, not physics, so leave it in Update?
        UpdateViewDirection();
    }

    void FixedUpdate()
    {
        // apply additional gravity 
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        // apply user input 
        HandleJump();
        HandleMovement();
    }
    
    // ------ View directions methods -------
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
    
    // ------ Move methods -------
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
        
        UpdateFootstepDistance(playerVelocity);
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

    private void UpdateFootstepDistance(Vector3 playerVelocity)
    {
        // remove vertical velocity axis
        playerVelocity.y = 0; 
        footstepDistanceCounter += playerVelocity.magnitude * Time.fixedDeltaTime;
        if (footstepDistanceCounter >= footstepDistance)
        {
            // play audio and wrap distance back to zero to prevent deviations
            characterAudio.PlayFootstep();
            footstepDistanceCounter -= footstepDistance;
        }
    }
   
    // ------ Jump methods ------- 
    private void HandleJump()
    {
        if (startJump)
        {
            characterAudio.PlayJump();
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            startJump = false;
            isJumping = true;
        } else if (isJumping)
        {
            // TODO - did we end the jump? 
            // Are we still jumping - acceleration of jump can come here 
        }
        
    }
   
   // ========================================
   // ================ LISTENERS ============
   void OnCollisionEnter(Collision collision)
   {
       if (collision.gameObject.tag == "WalkableSurface")
       {
           if (isJumping)
           {
               isJumping = false;
               characterAudio.PlayLand();
           }
           
       }
   }

    
    
    // ------ InputSystem methods ------- 
    void OnMove (InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    void OnLook(InputValue lookValue)
    {
        lookInput = lookValue.Get<Vector2>();
    }

    void OnJump(InputValue jumpValue)
    {
        if (jumpValue.isPressed)
        {
            if(!isJumping) startJump = true; 
        }
    }
    // TODO - OnSprint, OnInteract, OnJump, OnAttack, ...  

}
