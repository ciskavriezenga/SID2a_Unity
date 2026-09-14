# Movement

## Rigidbody types
- Dynamic colliders can move and have a Rigidbody attached
  - Standard Rigidbodies are moved using physics' forces, thus amongst others can be affected by gravity
    - use when you want objects to behave according to the laws of physics, e.g. projectiles with arcs, rolling objects.

  - Kinematic Rigidbodies are moved using their transform, unaffected by forces like gravity.
    - use for moving objects when you need precice control over your object position or do not want it to be moved when collided with other objects. E.g. obstacles that move in a pattern like giant swinging pendulum obstacles, heavy enemies that should not be pushed around or knocked back after collision with player.

## Movement player in 3D space on surface - standard rigidbody
Steps:
1. Transform moveInput.x and moveInput.y into movement in the current player direction. See method RetrieveWorldspaceMoveInput in PlayerController
2. Transform worldspaceMoveInput to target velocity by considering the player speed setting.
3. Update player current velocity with the target velocity, 'move slowly to target velocity'. Either by using Lerp (linear interpolation --> exponential curve) or move vector incrementally towards target vector (same delta each time) with Vector3.MoveTowards
4. Update player rigidbody linearVelocity by assigning the new velocity.
  `rb.linearVelocity = playerVelocity;`

RigidBody.linearVelocity is in units per seconds. Thus
when we want to track the distance traveled, e.g. to enable triggering the sound of footsteps, we can use  Time.fixedDeltaTime. Since velocity of a standard rigidbody concerns physics, which should be updated in FixedUpdate. Thus, fixtedDeltaTime can be used to retrieve the delta time for that fixedUpdate frame to transform the linearVelocity to a distance vector.
See PlayerController.UpdateFootstepDistance in this project.
