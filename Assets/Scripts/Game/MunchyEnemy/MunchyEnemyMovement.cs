using UnityEngine;

public class MunchyEnemyMovement : MonoBehaviour
{
    private Vector2 direction;
    private SpriteRenderer spriteRenderer;
    private RaycastHit2D raycast;
    private const float RaycastDistance = 0.6F;
    public AudioSource enemyDead;

    // Start is called before the first frame update
    private void Start()
    {
        direction = GetStartingDirection();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateDirectionSprite();
        DrawRaycast();
        transform.Translate(direction * Time.deltaTime);
    }

    private void DrawRaycast()
    {
        raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.34F), direction, RaycastDistance);
        if (raycast.collider != null && raycast.collider.gameObject.tag != "AudioAreaTrigger")
        {
            //flips character direction
            direction = -direction;
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

    private Vector2 GetStartingDirection()
    {
        if (Random.Range(0, 2) == 0)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.right;
        }
    }
}