using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float dirX;
    private const float Speed = 9F, WalkAcceleration = 75F, AirAcceleration = 30F, GroundDeceleration = 75F, JumpForce = 7f;
    private bool isDoubleJump;
    private Rigidbody2D playerRigidBody;
    private Vector2 velocity;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public AudioSource jumpSound;

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
    private void Update()
    {
        UpdateAnimationStates();
        dirX = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(dirX * JumpForce, playerRigidBody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            PerformJump();
        }

        float acceleration = PlayerGroundedCheck.Instance.IsGrounded(transform.position) ? WalkAcceleration : AirAcceleration;
        float deceleration = PlayerGroundedCheck.Instance.IsGrounded(transform.position) ? GroundDeceleration : 0;

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
        if (PlayerGroundedCheck.Instance.IsGrounded(transform.position))
        {
            jumpSound.Play();
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
            isDoubleJump = true;
        }
        else if (!PlayerGroundedCheck.Instance.IsGrounded(transform.position) && isDoubleJump)
        {
            jumpSound.Play();
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
            isDoubleJump = false;
        }
    }

    private void UpdateAnimationStates()
    {
        MovementState animState;

        if (!GameManager.Instance.isPlayerDead)
        {
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

            if (playerRigidBody.velocity.y > .1F && PlayerGroundedCheck.Instance.IsGrounded(transform.position))
            {
                animState = MovementState.running;
            }
            else if (playerRigidBody.velocity.y > .1F && !PlayerGroundedCheck.Instance.IsGrounded(transform.position))
            {
                animState = MovementState.jumping;
            }
            else if (playerRigidBody.velocity.y < -.1F && PlayerGroundedCheck.Instance.IsGrounded(transform.position))
            {
                animState = MovementState.running;
            }
            else if (playerRigidBody.velocity.y < -.1F && !PlayerGroundedCheck.Instance.IsGrounded(transform.position))
            {
                animState = MovementState.falling;
            }
            anim.SetInteger("playerStateIndex", (int)animState);
        }
    }
}