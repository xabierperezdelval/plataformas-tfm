using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour
{
    private GameObject playerObject, collisionObject;
    private SpriteRenderer playerSpriteRenderer;
    private Vector2 bulletDirection;
    private const float bulletSpeed = 14.5F;

    private void OnEnable()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
        GetPlayerFacingDirection();
    }

    private void GetPlayerFacingDirection()
    {
        if (!playerSpriteRenderer.flipX)
        {
            bulletDirection = transform.right;
        }
        else
        {
            bulletDirection = -transform.right;
        }
    }

    private void Update()
    {
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector2(0F, -10F);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Terrain":
                gameObject.SetActive(false);
                break;

            case "StompableEnemy":
                PerformEnemyDeadBehaviour(collision.gameObject, 100);
                break;

            case "NonStompableEnemy":
                PerformEnemyDeadBehaviour(collision.gameObject, 300);
                break;

            case "PickupItem":
                break;

            case "Shield":
                break;

            case "MonsterWall":
                break;

            case "AudioAreaTrigger":
                break;

            default:
                gameObject.SetActive(false);
                break;
        }
    }

    private void PerformEnemyDeadBehaviour(GameObject collisionObject, int score)
    {
        gameObject.SetActive(false);
        if (collisionObject.tag == "StompableEnemy")
        {
            collisionObject.GetComponent<MunchyEnemyMovement>().enemyDead.Play();
        }
        else
        {
            collisionObject.GetComponent<BullyEnemyMovement>().enemyDead.Play();
        }

        collisionObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        collisionObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        collisionObject.GetComponent<BoxCollider2D>().enabled = false;
        collisionObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4F);
        foreach (var c in collisionObject.GetComponentsInChildren<BoxCollider2D>())
        {
            c.enabled = false;
        }
        Destroy(collisionObject, 3.5F);
        GameManager.Instance.score += score;
    }
}