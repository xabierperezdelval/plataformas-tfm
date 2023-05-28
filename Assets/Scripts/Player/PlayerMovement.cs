using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float dirX;
    private bool isDoubleJump;
    private Rigidbody2D playerRigidBody;
    private Vector2 velocity;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    const float jumpForce = 7f;
    float speed = 9, walkAcceleration = 75, airAcceleration = 30, groundDeceleration = 70;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dirX = Input.GetAxis("Horizontal");
        playerRigidBody.velocity = new Vector2(dirX * jumpForce, playerRigidBody.velocity.y);


        if (Input.GetButtonDown("Jump"))
        {
            PerformJump();
        }

        float acceleration = PlayerCollisions.Instance.IsGrounded(transform.position) ? walkAcceleration : airAcceleration;
        float deceleration = PlayerCollisions.Instance.IsGrounded(transform.position) ? groundDeceleration : 0;

        //player is moving
        if (dirX != 0)
        {
            if (dirX < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            velocity.x = Mathf.MoveTowards(velocity.x, speed * dirX, acceleration * Time.deltaTime);
            anim.SetBool("isRunning", true);

        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
            anim.SetBool("isRunning", false);
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;
    }

    private void PerformJump()
    {
        if (PlayerCollisions.Instance.IsGrounded(transform.position))
        {
            isDoubleJump = true;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
        }
        else if (!PlayerCollisions.Instance.IsGrounded(transform.position) && isDoubleJump)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpForce);
            isDoubleJump = false;
        }
    }
}
