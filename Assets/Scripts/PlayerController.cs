using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    private Rigidbody rb;
    private Vector2 moveInput = new Vector2(0, 0);
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
        HandleCharacterMovement();
    }
    /*
     * Update - OR use FixedUpdate
     * Update is called once per frame
     * FixedUpdate - when using rigidBody and physics, not implemented currently
     * however, physics in Updatae is not a problem if you multiply with Time.deltaTime
     * (In FixedUpdate Time.deltaTime is a constant, hence the 'fixed' update, so not necessary there)
     */ 
    
    void HandleCharacterMovement()
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
        print(moveInput.y); 
        
        // move character 
        transform.position += transform.forward * moveInput.y * speed * Time.deltaTime;
        transform.position += transform.right * moveInput.x * speed * Time.deltaTime;
        
        // TODO - check position in World and update music if required
        // NOTE - use the position for this, can we retrieve on top of which plane we stand  
        
        
        /*
         * Another option is to transform the moveInput to worldSpace moveInput:
         *      Vector3 worldspaceMoveInput = transform.TransformVector(moveInput)         
         *
         * Even more sofisticated is using a velocity, and using input to increase
         * or the lack of input to decrease the velocity, e.g.:
         *      Vector3 targetVelocity = worldspaceMoveInput * speed;
         *      playerVelocity = Vector3.Lerp(playerVelocity, targetVelocity, accelerationIntensity * Time.deltaTime);
         * whereby accelerationIntensity affects the player acceleration & deceleration, 
         * low --> slowly, high --> quick
         *
         * and then add it to position, thus without the transform.forward but as 3D Vector
         *
         * TODO - try this out --> interesting to connect to sound of footsteps,
         * since we can retrieve the magnitude of the velocity
         * CharacterVelocity.magnitude
         *
         * Also see the PlayerCharacterController in the FPS_microgame_learning Unity tutorial project
         */
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

}
