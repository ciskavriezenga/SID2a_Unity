# Monobehaviour methods

## Update - OR use FixedUpdate
- Update is called once per frame
  - Physics in Update is not a problem if you multiply with Time.deltaTime
- FixedUpdate - when using rigidBody and physics
  - In FixedUpdate Time.deltaTime is a constant, hence the 'fixed' update, so not necessary there.
