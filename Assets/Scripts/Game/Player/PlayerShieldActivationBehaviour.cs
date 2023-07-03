using UnityEngine;

public class PlayerShieldActivationBehaviour : MonoBehaviour
{
    public GameObject shield;
    public AudioSource shieldSound;
    private bool areShieldsUp;

    // Start is called before the first frame update
    private void Start()
    {
        areShieldsUp = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.isShieldPicked)
        {
            if (Input.GetButton("Fire2") && GameManager.Instance.currentPower > 0)
            {
                shield.SetActive(true);
                if (areShieldsUp)
                {
                    shieldSound.Play();
                    areShieldsUp = false;
                }

                GameManager.Instance.currentPower -= 0.4F;
            }
            else
            {
                shield.GetComponent<ShieldAppearBehaviour>().shieldButtonReleased = true;
                if (GameManager.Instance.currentPower > 2)
                {
                    areShieldsUp = true;
                }
                if (GameManager.Instance.currentPower < 100)
                {
                    GameManager.Instance.currentPower += 0.08F;
                }
            }
        }
    }
}