using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonScreenManager : MonoBehaviour
{
    private float score;
    public TMPro.TextMeshProUGUI finalScoreText;
    public AudioSource startGameSound;
    public AudioSource gameWonMusic;
    private bool isFading, soundPlayedOnce;

    // Start is called before the first frame update
    private void Start()
    {
        soundPlayedOnce = true;
        GetComponent<FadeInOutEffect>().isFadeTime = true;
        GetComponent<FadeInOutEffect>().fadeType = "out";
        isFading = false;
        score = PlayerPrefs.GetFloat("currentGameScore");
        finalScoreText.text = "Final score: " + score;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            GetComponent<FadeInOutEffect>().isFadeTime = true;
            isFading = true;
            GetComponent<FadeInOutEffect>().fadeType = "in";
            if (soundPlayedOnce)
            {
                startGameSound.Play();
                soundPlayedOnce = false;
            }
            StartCoroutine(ChangeToMenuScene());
        }
    }

    private void FixedUpdate()
    {

        if (isFading)
        {
            gameWonMusic.volume -= 0.05F;
        }
    }

    private IEnumerator ChangeToMenuScene()
    {
        yield return new WaitForSeconds(1.6F);
        SceneManager.LoadScene("MainMenuScene");
    }

}
