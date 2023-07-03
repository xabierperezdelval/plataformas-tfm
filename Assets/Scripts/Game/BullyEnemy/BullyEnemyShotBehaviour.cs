using UnityEngine;

public class BullyEnemyShotBehaviour : MonoBehaviour
{
    private GameObject enemyObject;
    private SpriteRenderer enemySpriteRenderer, selfSpriteRenderer;
    private Vector2 bulletDirection;
    private float shotInstanceOffsetAssign, shotInstanceOffsetInitialValue = 0.9F;
    private const float bulletSpeed = 7F;
    public AudioSource playerHitSound;
    private bool isPlayerFacingRight;

    private void OnEnable()
    {
        playerHitSound = GameObject.Find("GameSoundsControllerObject").transform.Find("PlayerHit").GetComponent<AudioSource>();
        enemyObject = transform.parent.gameObject;
        enemySpriteRenderer = enemyObject.GetComponent<SpriteRenderer>();
        selfSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        CheckShotDirection();
        SetInitialPosition();
    }

    private void CheckShotDirection()
    {
        if (!enemySpriteRenderer.flipX)
        {
            bulletDirection = -transform.right;
            shotInstanceOffsetAssign = -shotInstanceOffsetInitialValue;
            selfSpriteRenderer.flipX = true;
        }
        else
        {
            bulletDirection = transform.right;
            shotInstanceOffsetAssign = shotInstanceOffsetInitialValue;
            selfSpriteRenderer.flipX = false;
        }
    }

    private void SetInitialPosition()
    {
        transform.position = new Vector2(transform.position.x + shotInstanceOffsetAssign, transform.position.y);
    }

    private void Update()
    {
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!InvincibilityBehaviour.Instance.invincibleFrames)
                {
                    playerHitSound.Play();
                    InvincibilityBehaviour.Instance.invincibleFrames = true;
                    isPlayerFacingRight = collision.gameObject.GetComponent<SpriteRenderer>().flipX;
                    Vector2 appliedForce = !isPlayerFacingRight ? new Vector2(-25F, 1.8F) : new Vector2(25F, 1.8F);
                    collision.GetComponent<Rigidbody2D>().AddForce(appliedForce, ForceMode2D.Impulse);
                    GameManager.Instance.currentHealth -= 15;
                    Destroy(gameObject);
                }
            }
            else if (collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Shield")
            {
                Destroy(gameObject);
            }
        }
    }
}