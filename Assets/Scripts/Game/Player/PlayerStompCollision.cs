using UnityEngine;

public class PlayerStompCollision : MonoBehaviour
{
    private RaycastHit2D raycast;
    private const float RaycastDistance = 0.5F;
    private Rigidbody2D playerRigidBody;
    public AudioSource enemyDeadSound;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        raycast = Physics2D.Raycast(transform.position, Vector2.down, RaycastDistance);
        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.tag == "StompableEnemy")
            {
                enemyDeadSound.Play();
                raycast.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                raycast.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                raycast.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                raycast.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4F);
                Destroy(raycast.collider.gameObject, 3.5F);
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 5.5F);
                foreach (var c in raycast.collider.gameObject.GetComponentsInChildren<BoxCollider2D>())
                {
                    c.enabled = false;
                }
                GameManager.Instance.score += 150;
            }
        }
    }
}