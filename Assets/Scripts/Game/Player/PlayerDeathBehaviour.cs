using UnityEngine;

public class PlayerDeathBehaviour : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerCollider;
    private SpriteRenderer playerSprite;
    private Animator playerAnimator;
    public Sprite deadSprite;
    public Camera gameCamera;
    public AudioSource playerDeadSound;
    public PlayerMovement playerMovementScript;
    public PlayerShotInstance playerShotInstanceScript;
    private bool soundPlayedOnce;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();
        playerMovementScript = GetComponent<PlayerMovement>();
        playerShotInstanceScript = GetComponent<PlayerShotInstance>();
        soundPlayedOnce = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.isPlayerDead)
        {
            if (soundPlayedOnce)
            {
                playerDeadSound.Play();
                soundPlayedOnce = false;
            }

            playerMovementScript.enabled = false;
            playerShotInstanceScript.enabled = false;
            playerAnimator.enabled = false;
            playerRigidBody.isKinematic = true;
            playerRigidBody.gravityScale = -0.02F;
            playerCollider.enabled = false;
            playerSprite.sprite = deadSprite;
            gameCamera.transform.parent = null;
            Destroy(gameObject, 5F);
        }
    }
}