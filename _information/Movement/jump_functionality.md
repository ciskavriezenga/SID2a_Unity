# Jump functionality

## First thoughts
How to add a jump action to the player?
And add sound to jump and touching the ground?

Think first: what do we need?
- Know when the spacebar is pressed --> we use the InputSystem, this is easy
- Trigger a jump when spacebar is pressed --> create a jump method in our Player script
- Jump up let gravity _do it's job_ :
  - start jump, somehow (?) move upwards + trigger jump sample
  - disable jump when jumping
  - know when we `touch the ground` again + trigger landing sample

Main question: How to implement the jump movement?

## Research options - "How to implement the jump movement?"
Multiple sources to consult, e.g.:
- many tutorials on youtube, e.g. https://youtu.be/V5x9q433rmE?si=Qkw8IsmpNtlS8um0
- reddit, e.g. https://www.reddit.com/r/Unity3D/comments/178o14a/is_there_a_way_to_make_my_player_jump_in_unity_3d/
- gamedevbeginner.com - https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/

However - some things to consider:
- are the examples using the new input system? Use "Unity 6" in your search to ensure you focus on the latest Unity versions, might as well add "InputSystem" to search
- is there a source that is written by Unity? By adding "unity learn" --> https://learn.unity.com/pathway/junior-programmer/unit/sound-and-effects/tutorial/lesson-3-1-jump-force-2, scrolling through it; no InputSystem in the code, so be aware! This is an old tutorial. And - OnCollission is used for isGrounded check, hmm not what we want!

Ok, let's recheck other sources. Text is preferred above video, so starting with  https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/ Pretty good! Nice overview!
- Many options!
- Preferred one after skimming all: Kinematic RigidBody, thus a RigidBody to which Unity physics are not applied. But let's first test it with the wall, will the player still be kept from falling by the wall?  NO - we can walk through the wall, hmmm... no go for now, cause all collissions needs to be manually scripted / adding another rb.

## Summary options
- RigidBody, thus using physics:
  - Velocity alteration by AddForce method,
    - Jump alteration, speed:
      - In 2D: change Gravity Scale on ridid body
      - In 3D: use a float that serves as gravity scale
        - high gravity scale --> issue though, fall through surface:
        set Collision Detection in RigidBody component from Discrete to Continuous,
        also, physics time step can be reduced, to increase simulation precision (better leave it at default though)

      ```csharp
      // Snippet below is based on source https://gamedevbeginner.com/how-to-jump-in-unity-with-or-without-physics/, but already slightly altered
      // in Update track key press and set startJump to True
      // in FixedUpdate
      if (startJump)
      {
          rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
          startJump = false
      }
      private void FixedUpdate()
      {
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
      }
      ```


    - jump alteration curve:
      - apply a higher gravity when player is moving downwards (falling)
      - jump to a specific height - calculate the jump velocity with formula
        velocity = sqrt(-2.0f * gravity * height)
        (note: hight often slightly less then targeted height )
      - variable height by tracking jumpTime depending on duration jump press
      and while jumping is pressed alter the rb velocity, when jumpTime exceeds
      a predefined duration - simply stop jumping and thus stop alteration of velocity
      - variable height + maximum height --> use AddForce again, start full jump.
      And when jump is canceled / max is reached, AddForce downward; the so called cancel rate
      - Also see: https://www.davetech.co.uk/gamedevplatformer
  - Smooth physics-based jumping --> RB component - set Interpolate to Interpolate, to smooth out the physics-based movements
- without using physics:
  - use the transform.Translate
  ```csharp
  velocity += gravity * Time.deltaTime;
  if (jumpAction){
      velocity = jumpForce;
  }
  transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
  ```
  Note: gravity is an acceleration (m/s^2), hence the two times multiplication with time (derivative)
  - adapt the jump by adding gravityScale:
    ```csharp
    velocity += gravity * gravityScale *Time.deltaTime;
    ```
  - Need to check if you touched the ground! Options:
    - Raycast
    - Overlap function
    Then, when touched the ground stop jump. But ... this can result in strange end position, e.g. partly through the floor. Thus, need to reposition. But hey, then we again use transform and do not use physics and need to handle object collision? --> use Character controller
- Character controller component instead of RigidBody.
  This is easy peasy, simply use move method to move player, instead of transform.Translate
  But here it comes; the interactions are less precise compared to RididBody physics.
  _"Generally speaking, if physics interactions are important in your game, the Character Controller may not do everything you want it to in the way that you want it to, and you may be better off using Unity’s physics system, by adding a Rigidbody component and making it move."_
- Kinematic RigidBody is another option, it is not moved by physics. You can move it with
  the Rigidbody.MovePosition method, works similar as the transform.
  However, Kinematic RB are not obstructed by static colliders, thus need a solution for that. This can be done with using raycast or an overlap method. by checking when the RB collides with another object using the On Collission messages. To enable OnCollission with Kinematic RB, you do need to go to Project Settings, Physics Menu and under Contact Pairs Mode - enable Kinematic Static Pairs
  Why not move an object without RB? --> bad practice ... _"because moving a Collider without using a Rigidbody to do it can be bad for performance."_
- Final option: Package Kinematic Character Controller

## Choose a solution from the options
- CharacterController: It is a simple project, thus is a  good option.
- RB & physics: good for purpose of learning and better results
- Without physics: only necessary if you have a good reason to not use the physics system, e.g.:
  - need a specifc type of movement that is not easy to create with physics
  - improve performance by avoiding physics calculations, or do not need many of the physics system features
  Note: needs to reset the position to the surface at end of jump,
- Kinematic RigidBody: allows "jump and move, with the precision of explicit,
Transform-based movement, but with a physics object",
downside: needs manual collision tracking
- Ready-made options like the 'Kinematic Character Controller' from asset store:
downside: dependency on third party package?

For this project: RB & physics, good learning option!
Also, let's try to discover what is most oftend used in projects at HKU Games! @T?

## Implement chosen solution
**Steps**
1. We are currently  using transform to apply movement - let's change this into using velocity first, but let's first clean up the large method
2. Track jump key press and set a startJump boolean to true in Update function
3. Add jumpAmount and gravityScale floats to Player class
4. Implement the jump and gravity AddForce calls
5. Implement an isJumping boolean that we set to True after starting Jump and
set to false again after ending the jumping, in an EndJump method. For the latter,
we can use OnColission and check if the collision is with a floor surface.
We might need to add a script to all 'jumpable objects', or maybe we can add a Unity tag and check that flag.
6. In both the start and end jump method trigger audio playback!
