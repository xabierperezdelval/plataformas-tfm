using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float dirX, dirY;
    private const float Speed = 9F, WalkAcceleration = 75F, AirAcceleration = 30F, GroundDeceleration = 75F, JumpForce = 7f;
    private bool isDoubleJump;
    private Rigidbody2D playerRigidBody;
    private Vector2 velocity;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private enum MovementState
    { idle, running, jumping, falling };

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateAnimationStates();
        dirX = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(dirX * JumpForce, playerRigidBody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            PerformJump();
        }

        float acceleration = PlayerCollisions.Instance.IsGrounded(transform.position) ? WalkAcceleration : AirAcceleration;
        float deceleration = PlayerCollisions.Instance.IsGrounded(transform.position) ? GroundDeceleration : 0;

        //player is moving
        if (dirX != 0F)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, Speed * dirX, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
    }

    private void PerformJump()
    {
        if (PlayerCollisions.Instance.IsGrounded(transform.position))
        {
            isDoubleJump = true;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
        }
        else if (!PlayerCollisions.Instance.IsGrounded(transform.position) && isDoubleJump)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
            isDoubleJump = false;
        }
    }

    private void UpdateAnimationStates()
    {
        MovementState animState;

        if (dirX != 0F)
        {
            if (dirX < 0F)
            {
                spriteRenderer.flipX = true;
                animState = MovementState.running;
            }
            else
            {
                spriteRenderer.flipX = false;
                animState = MovementState.running;
            }
        }
        else
        {
            animState = MovementState.idle;
        }

        if (playerRigidBody.velocity.y > .1F)
        {
            animState = MovementState.jumping;
        }
        else if (playerRigidBody.velocity.y < -.1F)
        {
            animState = MovementState.falling;
        }
        anim.SetInteger("playerStateIndex", (int)animState);
    }
}