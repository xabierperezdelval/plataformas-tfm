using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private float _highScore;
    public TMPro.TextMeshProUGUI highScoreText;
    public AudioSource startGameSound;
    public AudioSource menuMusic;
    private bool isFading, soundPlayerOnce;

    // Start is called before the first frame update
    private void Start()
    {
        soundPlayerOnce = true;
        GetComponent<FadeInOutEffect>().isFadeTime = true;
        GetComponent<FadeInOutEffect>().fadeType = "out";
        isFading = false;
        _highScore = PlayerPrefs.GetFloat("highScore");
        highScoreText.text = "High score: " + _highScore;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            GetComponent<FadeInOutEffect>().isFadeTime = true;
            GetComponent<FadeInOutEffect>().fadeType = "in";
            isFading = true;
            if (soundPlayerOnce)
            {
                startGameSound.Play();
                soundPlayerOnce = false;
            }

            StartCoroutine(ChangeToGameScene());
        }
    }

    private void FixedUpdate()
    {
        if (isFading)
        {
            menuMusic.volume -= 0.05F;
        }
    }

    private IEnumerator ChangeToGameScene()
    {
        yield return new WaitForSeconds(1.6F);
        SceneManager.LoadScene("GameScene");
    }
}