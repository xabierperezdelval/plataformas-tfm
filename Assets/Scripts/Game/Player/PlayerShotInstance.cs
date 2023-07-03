using UnityEngine;

public class PlayerShotInstance : MonoBehaviour
{
    private Vector2 playerPos;
    public GameObject playerShot;
    public GameObject playerBulletPool;
    public AudioSource playerShotSound;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            playerBulletPool.GetComponent<PlayerBulletPool>().ShootBullet();
            if (playerShot != null)
            {
                playerShotSound.Play();
            }
        }
    }
}