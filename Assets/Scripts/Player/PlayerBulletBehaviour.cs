using UnityEngine;

public class PlayerBulletBehaviour : MonoBehaviour
{
    private GameObject playerObject;
    private SpriteRenderer playerSpriteRenderer;
    private Vector2 bulletDirection;
    private const float bulletSpeed = 14.5F;

    private void OnEnable()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
        bulletDirection = transform.right;
        if (!playerSpriteRenderer.flipX)
        {
            bulletDirection = transform.right;
        }
        else
        {
            bulletDirection = -transform.right;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(bulletDirection * bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    {
        gameObject.transform.position = new Vector2(0F, -10F);
        gameObject.SetActive(false);
    }
}