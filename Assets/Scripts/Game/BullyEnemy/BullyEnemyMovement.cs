using UnityEngine;
using Random = UnityEngine.Random;

public class BullyEnemyMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private RaycastHit2D edgeDetectionRaycast;
    private Rigidbody2D body2D;
    private float movementCounter;
    private bool shotAllowed;
    private const float EdgeDetectionRaycastDistance = 0.6F;
    public GameObject fireball;
    public Vector2 direction;
    public AudioSource bullyShotSound, enemyDead;
    public bool isPlayerNearby, isSentinel;

    private void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        shotAllowed = true;
        direction = GetStartingDirection();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ResetMovementCounter();
        CheckIfSentinelMode();
    }

    private void Update()
    {
        if (!isSentinel)
        {
            DrawObstacleAndEdgeDetectionRaycast();
        }
        UpdateDirectionSprite();
        SetMovementCycles();
    }

    private void CheckIfSentinelMode()
    {
        if (isSentinel)
        {
            body2D.isKinematic = true;
        }
    }

    private void ResetMovementCounter()
    {
        movementCounter = !isSentinel ? Random.Range(1F, 2.3F) : Random.Range(0.5F, 0.7F);
    }

    private void ExecuteShot()
    {
        if (shotAllowed && gameObject.GetComponent<BoxCollider2D>().enabled)
        {
            GameObject fireballInstance = Instantiate(fireball, transform.position, Quaternion.identity, transform);
            shotAllowed = false;
            CheckFireSound();
        }
        ResetMovementCounter();

    }

    private void CheckFireSound()
    {
        if (isPlayerNearby)
        {
            bullyShotSound.volume = 1;

        }
        else
        {
            bullyShotSound.volume = 0;
        }
        bullyShotSound.Play();
    }

    private void SetMovementCycles()
    {
        movementCounter -= Time.deltaTime;
        if (movementCounter > 0)
        {
            if (!isSentinel)
            {
                transform.Translate(direction * 2F * Time.deltaTime);
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            shotAllowed = true;
            anim.SetBool("isMoving", false);
            Invoke("ExecuteShot", 1F);
        }
    }

    private Vector2 GetStartingDirection()
    {
        if (isSentinel)
        {
            return Vector2.left;
        }
        if (Random.Range(0, 2) == 0)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.right;
        }
    }

    private void DrawObstacleAndEdgeDetectionRaycast()
    {
        edgeDetectionRaycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.34F), direction, EdgeDetectionRaycastDistance);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.34F), direction * EdgeDetectionRaycastDistance, Color.red);
        if (edgeDetectionRaycast.collider != null && edgeDetectionRaycast.collider.gameObject.tag != "AudioAreaTrigger")
        {
            OnObstacleHitChangeDirection();
            Debug.DrawRay(transform.position, direction * EdgeDetectionRaycastDistance, Color.green);
        }
    }

    private void UpdateDirectionSprite()
    {
        if (direction == Vector2.left)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnObstacleHitChangeDirection()
    {
        direction = -direction;
    }
}