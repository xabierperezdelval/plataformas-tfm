using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public LayerMask groundLayer;

    //creamos una instancia para manejar los par�metros desde otros objetos
    public static PlayerCollisions Instance { get; private set; }

    //prevenimos m�ltiples copias/instancias
    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    //checks if player is colliding the ground (below)
    public bool IsGrounded(Vector2 playerRayPos)
    {
        Vector2 position = playerRayPos;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, Instance.groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
