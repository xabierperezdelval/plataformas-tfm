using UnityEngine;

public class ShieldPickupBehaviour : MonoBehaviour
{
    public Transform[] movementPoints;
    private int i;
    public float smoothSpeed;
    public AudioSource shieldPickupSound;

    // Start is called before the first frame update
    private void Start()
    {
        transform.position = movementPoints[Random.Range(0, 2)].position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(transform.position, movementPoints[i].position) < 0.02F)
        {
            i++;
            if (i == movementPoints.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.Lerp(transform.position, movementPoints[i].position, Time.deltaTime * smoothSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shieldPickupSound.Play();
            GameManager.Instance.isShieldPicked = true;
            Destroy(gameObject);
        }
    }
}