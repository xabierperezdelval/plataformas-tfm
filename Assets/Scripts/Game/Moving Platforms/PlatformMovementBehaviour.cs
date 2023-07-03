using UnityEngine;

public class PlatformMovementBehaviour : MonoBehaviour
{
    public float platformSpeed;
    public Transform[] movementPoints;
    private int i;

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

        transform.position = Vector2.MoveTowards(transform.position, movementPoints[i].position, platformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}