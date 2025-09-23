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




public class PlayerController : MonoBehaviour
{
    public Transform head;
    public Camera camera;
    public float mouseSentivity = 0.5f;
    public float speed = 0.0f;
    public float accelerationIntensity = 1.0f;
    
    private Rigidbody rb;
    private Vector2 moveInput = new Vector2(0, 0);
    Vector3 moveAxis;
    private Vector2 lookInput = new Vector2(0, 0);
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
        HandleMoveInput();
    }

    void FixedUpdate()
    {
        Move();
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

    void HandleMoveInput()
    {  
        // NOTE: moveInput is a Vector2 representing the 2d input of
        // - the wasd or arrow buttons,
        // - the movement joystick 

        moveAxis = new Vector3(moveInput.x, 0, moveInput.y);
        moveAxis = Vector3.ClampMagnitude(moveAxis, 1);
    }

   
    
    void Move()
    {
        // NOTE 2: since we are working with RB and physics,
        // this method needs to be called by FixedUpdate instead of Update.
        
        // transform the moveInput to worldspace so it is directed according to player's direction
        Vector3 worldspaceMoveInput = RetrieveWorldspaceMoveInput();

        // calculate new velocity 
        Vector3 targetVelocity = worldspaceMoveInput * speed;
        Vector3 playerVelocity = rb.linearVelocity;
#if False
        // by applying linear interpolation with Lerp each frame --> exponentional curve
        //
        playerVelocity = Vector3.Lerp(playerVelocity, targetVelocity, Time.fixedDeltaTime * accelerationIntensity);
#else
        // linear progression by maxDistanceDelta, third parameter of MoveTowards
        playerVelocity = Vector3.MoveTowards(playerVelocity, targetVelocity, Time.fixedDeltaTime * accelerationIntensity);
#endif
        rb.MovePosition(rb.position + (worldspaceMoveInput * speed * Time.fixedDeltaTime));
        // move rigidbody
        rb.linearVelocity = playerVelocity;
    }
  
    Vector3 RetrieveWorldspaceMoveInput()
    {
        // only consider camera’s horizontal rotation (yaw)
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();
        
        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();
        
        return forward * moveAxis.z + right * moveAxis.x;
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
    
    // TODO - OnSprint, OnInteract, OnJump, OnAttack, ...  

}
