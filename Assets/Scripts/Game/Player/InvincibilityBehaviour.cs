using System.Collections;
using UnityEngine;

public class InvincibilityBehaviour : MonoBehaviour
{
    public bool invincibleFrames;
    private float counter = 3F;
    private SpriteRenderer spriteRenderer;
    public static InvincibilityBehaviour Instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (invincibleFrames)
        {
            StartCoroutine(BecomeTemporarilyInvincible());
        }
    }

    private void SetRendererTo(bool invisibleValue)
    {
        spriteRenderer.enabled = invisibleValue;
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        for (float i = 0; i < counter; i += counter)
        {
            if (spriteRenderer.enabled)
            {
                SetRendererTo(false);
            }
            else
            {
                SetRendererTo(true);
            }
            yield return new WaitForSeconds(3F);
            SetRendererTo(true);
            invincibleFrames = false;
            counter = 3F;
        }
    }
}