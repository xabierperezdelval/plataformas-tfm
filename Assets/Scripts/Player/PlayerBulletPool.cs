using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    public int bulletPoolSize = 10;
    /* Tamaño array de objetos para reciclar valor estimado "a ojo", aconsejable los posibles sin reusar uno que no esté desactivado */

    public GameObject bullet;
    /*Referencia temporal para gestión de llenado del array con objetos prefabs */
    private GameObject[] bullets;
    //Array de objetos a reciclar 
    public int shootNumber = -1;
    /* Número entero que lleva la cuenta de qué posición del array toca activar y gestionar */
    private SpriteRenderer playerSpriteRenderer;
    private GameObject playerObject;
    private float shotInstanceOffset;
    void Start()
    {
        //Creamos array con un tamaño igual al de la variable int primera
        bullets = new GameObject[bulletPoolSize];
        playerObject = GameObject.FindWithTag("Player");
        playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
        Debug.Log(bulletPoolSize);
        /*De forma secuencial gracias a un bucle for creamos todas las balas, recordad que estas comienzan desactivadas, con lo cual no se ejecutará su script y saldrán todas disparadas a la vez. Fijaros también en la posición de creación: el valor -10 de la “x” hace que estén fuera de la escena por seguridad. */

        for (int i = 0; i < bulletPoolSize; i++)
        {
            bullets[i] = Instantiate(
                bullet, new Vector2(0f, -10f), Quaternion.identity);
        }
    }

    private void getFacingDirection()
    {
        if (!playerSpriteRenderer.flipX)
        {
            shotInstanceOffset = 1.2F;
        }
        else
        {
            shotInstanceOffset = -1.2F;
        }
    }
    public void ShootBullet()
    {
        //Cada vez que disparemos el “puntero” del array, aumenta en uno para que el siguiente disparo señale a la siguiente bala del array. 
        
        shootNumber++;
        //En el caso de que el puntero supere el número de posiciones del array, vuelve a 0 para seguir con el proceso.
        if (shootNumber > 9)
        {
            shootNumber = 0;
        }
        //Ponemos la bala, desactivada aún, en la posición del objeto "GunObject"

        //¡Activamos la bala!
        if (!bullets[shootNumber].activeSelf)
        {
            getFacingDirection();
            bullets[shootNumber].transform.position = new Vector2(playerObject.transform.position.x + shotInstanceOffset, playerObject.transform.position.y);
            bullets[shootNumber].SetActive(true);
        }
    }
}
