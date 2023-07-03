using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAreaPlayerDetection : MonoBehaviour
{
    private BullyEnemyMovement bullyEnemyMovementScript;
    private void Start()
    {
        bullyEnemyMovementScript = transform.parent.GetComponent<BullyEnemyMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bullyEnemyMovementScript.isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bullyEnemyMovementScript.isPlayerNearby = false;
        }
    }
}
