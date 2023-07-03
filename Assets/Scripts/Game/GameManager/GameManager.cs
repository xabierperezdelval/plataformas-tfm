using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;

    public float timeLeft = 200F, maxHealth, maxPower, currentHealth, currentPower;
    public TMPro.TextMeshProUGUI scoreValueText, timeValueText;
    public Image powerBarFrame, healthBarValue, powerBarValue;
    public bool isGoalReached, isShieldPicked, isPlayerDead, soundPlayerOnce;
    public AudioSource gameMusic, gameWonSound;
    public PlayerMovement playerMovement;
    public PlayerShotInstance playerShotInstance;
    public PlayerShieldActivationBehaviour playerShieldActivationBehaviour;
    private FadeInOutEffect fadeInOutEffectScript;

    public static GameManager Instance { get; private set; }

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
        AssignInitialValues();
    }

    // Update is called once per frame
    private void Update()
    {
        timeValueText.text = Math.Round(timeLeft, 0).ToString();
        scoreValueText.text = score.ToString();

        SetHealthPowerValues();

        SetTimeBehaviour();

        if (Input.GetButtonDown("Submit"))
        {
            PauseUnpauseState();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            ExitGame();
        }

        if (isGoalReached)
        {
            SetGoalReachedStatus();
        }

        if (currentHealth <= 0)
        {
            SetLifeDepletedStatus();
        }
        if (isShieldPicked)
        {
            SetShieldPickUpStatus();
        }
    }

    private void AssignInitialValues()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerShotInstance = GameObject.Find("Player").GetComponent<PlayerShotInstance>();
        playerShieldActivationBehaviour = GameObject.Find("Player").GetComponent<PlayerShieldActivationBehaviour>();
        fadeInOutEffectScript = GetComponent<FadeInOutEffect>();
        fadeInOutEffectScript.isFadeTime = true;
        fadeInOutEffectScript.fadeType = "out";
        isPlayerDead = false;
        isGoalReached = false;
        isShieldPicked = false;
        soundPlayerOnce = true;
        maxHealth = 100;
        maxPower = 100;
        currentHealth = 100;
        currentPower = 100;
        var barFrameColorValues = powerBarFrame.color;
        barFrameColorValues.a = 0.3F;
        powerBarFrame.color = barFrameColorValues;
        var barValueColorValues = powerBarValue.color;
        barValueColorValues.a = 0.3F;
        powerBarValue.color = barValueColorValues;
    }

    private void PauseUnpauseState()
    {
        soundPlayerOnce = true;
        if (soundPlayerOnce)
        {
            gameWonSound.Play();
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                playerMovement.enabled = false;
                playerShotInstance.enabled = false;
                playerShieldActivationBehaviour.enabled = false;
                gameMusic.volume = 0.15F;
            }
            else
            {
                Time.timeScale = 1;
                playerMovement.enabled = true;
                playerShotInstance.enabled = true;
                playerShieldActivationBehaviour.enabled = true;
                gameMusic.volume = 0.38F;
            }
        }
    }

    private void ExitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void SetHealthPowerValues()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = 100;
        }
        if (currentPower < 0)
        {
            currentPower = 0;
        }
        healthBarValue.fillAmount = currentHealth / maxHealth;
        powerBarValue.fillAmount = currentPower / maxPower;
    }

    private void SetTimeBehaviour()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            isPlayerDead = true;
            isShieldPicked = false;
            SetFinalScore(false);
            StartCoroutine(ChangeToGameOverScene());
        }
    }

    private void SetGoalReachedStatus()
    {
        fadeInOutEffectScript.isFadeTime = true;
        fadeInOutEffectScript.fadeType = "in";
        SetFinalScore(true);
        gameMusic.volume = 0;
        if (soundPlayerOnce)
        {
            gameWonSound.Play();
            soundPlayerOnce = false;
        }
        isGoalReached = false;

        StartCoroutine(ChangeToGameWonScene());
    }

    private void SetLifeDepletedStatus()
    {
        isPlayerDead = true;
        isShieldPicked = false;
        SetFinalScore(false);
        StartCoroutine(ChangeToGameOverScene());
    }

    private void SetShieldPickUpStatus()
    {
        var barFrameColorValues = powerBarFrame.color;
        barFrameColorValues.a = 1F;
        powerBarFrame.color = barFrameColorValues;
        var barValueColorValues = powerBarValue.color;
        barValueColorValues.a = 1F;
        powerBarValue.color = barValueColorValues;
    }

    private void SetFinalScore(bool isStageFinished)
    {
        if (isStageFinished)
        {
            for (int i = (int)timeLeft; i > -1; i--)
            {
                score = score + 100;
            }
        }
        PlayerPrefs.SetFloat("currentGameScore", score);
        if (score > PlayerPrefs.GetFloat("highScore"))
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
    }

    private IEnumerator ChangeToGameOverScene()
    {
        yield return new WaitForSeconds(1.6F);
        SceneManager.LoadScene("GameOverScene");
    }

    private IEnumerator ChangeToGameWonScene()
    {
        {
            yield return new WaitForSeconds(1.6F);
            SceneManager.LoadScene("GameWonScene");
        }
    }
}