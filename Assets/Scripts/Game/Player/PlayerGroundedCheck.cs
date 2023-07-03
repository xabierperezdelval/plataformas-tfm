using UnityEngine;

public class PlayerGroundedCheck : MonoBehaviour
{
    public LayerMask groundLayer;

    //creamos una instancia para manejar los parámetros desde otros objetos
    public static PlayerGroundedCheck Instance { get; private set; }

    //prevenimos múltiples copias/instancias
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