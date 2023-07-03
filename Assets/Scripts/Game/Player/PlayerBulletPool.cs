using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    public int bulletPoolSize = 10, shootNumber = -1;
    private float shotInstanceOffsetAssign, shotInstanceOffsetInitialValue = 1.2F;
    public GameObject bullet;
    private GameObject[] bullets;
    private GameObject playerObject;
    private SpriteRenderer playerSpriteRenderer;

    private void Start()
    {
        bullets = new GameObject[bulletPoolSize];
        playerObject = GameObject.FindWithTag("Player");
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets[i] = Instantiate(
                bullet, new Vector2(0f, -10f), Quaternion.identity);
        }
    }

    private void getFacingDirection()
    {
        if (!playerSpriteRenderer.flipX)
        {
            shotInstanceOffsetAssign = shotInstanceOffsetInitialValue;
        }
        else
        {
            shotInstanceOffsetAssign = -shotInstanceOffsetInitialValue;
        }
    }

    public void ShootBullet()
    {
        shootNumber++;
        if (shootNumber > 9)
        {
            shootNumber = 0;
        }

        if (!bullets[shootNumber].activeSelf)
        {
            getFacingDirection();
            bullets[shootNumber].transform.position = new Vector2(playerObject.transform.position.x + shotInstanceOffsetAssign, playerObject.transform.position.y);
            bullets[shootNumber].SetActive(true);
        }
    }
}