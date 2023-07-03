using UnityEngine;

public class PlayerPickupCollisions : MonoBehaviour
{
    public AudioSource goldCoinAndFruitSound;
    public AudioSource platinumCoinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "GoldCoin":
                goldCoinAndFruitSound.Play();
                GameManager.Instance.score += 100;
                Destroy(collision.gameObject);
                break;

            case "PlatinumCoin":
                platinumCoinSound.Play();
                GameManager.Instance.score += 500;
                Destroy(collision.gameObject);
                break;

            case "SmallFruit":
                goldCoinAndFruitSound.Play();
                if (GameManager.Instance.currentHealth == 100)
                {
                    GameManager.Instance.score += 100;
                }
                else
                {
                    GameManager.Instance.currentHealth += 15;
                }
                Destroy(collision.gameObject);
                break;

            case "LargeFruit":
                goldCoinAndFruitSound.Play();
                if (GameManager.Instance.currentHealth == 100)
                {
                    GameManager.Instance.score += 200;
                }
                else
                {
                    GameManager.Instance.currentHealth += 30;
                }
                Destroy(collision.gameObject);
                break;
            case "Goal":
                GameManager.Instance.isGoalReached = true;
                break;
            default:
                break;
        }
    }
}