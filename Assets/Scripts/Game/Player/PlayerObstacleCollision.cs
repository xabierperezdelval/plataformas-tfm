using UnityEngine;

public class PlayerObstacleCollision : MonoBehaviour
{
    public AudioSource playerHitSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (!InvincibilityBehaviour.Instance.invincibleFrames)
            {
                InvincibilityBehaviour.Instance.invincibleFrames = true;
                CheckHealthAndKnockback(25);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DamageZone")
        {
            if (!InvincibilityBehaviour.Instance.invincibleFrames)
            {
                InvincibilityBehaviour.Instance.invincibleFrames = true;
                CheckHealthAndKnockback(20);
            }
        }
    }

    private void CheckHealthAndKnockback(int damageDone)
    {
        playerHitSound.Play();
        var playerDirection = gameObject.GetComponent<SpriteRenderer>().flipX;
        Vector2 appliedForce = !playerDirection ? new Vector2(-25F, 1.8F) : new Vector2(25F, 1.8F);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<Rigidbody2D>().AddForce(appliedForce, ForceMode2D.Impulse);
        GameManager.Instance.currentHealth -= damageDone;
    }
}