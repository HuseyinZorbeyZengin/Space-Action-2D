using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLives = 3;

    public float respawnTime = 2f;

    public int currentScore;
    private int highScore = 500;

    public bool levelEnding;

    private int levelScore;

    public float waitForLevelEnd = 5f;

    public string nextLevel;

    private bool canPause;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLives = PlayerPrefs.GetInt("CurrentLives");
        UIManager.instance.livesText.text = "x " + currentLives;



        highScore = PlayerPrefs.GetInt("HighScore");
        UIManager.instance.hiScoreText.text = "Hi-Score: " + highScore;

        currentScore = PlayerPrefs.GetInt("CurrentScore");
        UIManager.instance.scoreText.text = "Score: " + currentScore;

        canPause = true;
    }

    void Update()
    {
        if (levelEnding)
        {
            PlayerController.instance.transform.position += new Vector3(PlayerController.instance.boostSpeed * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            PauseUnpause();
        }
    }

    public void KillPlayer()
    {
        currentLives--;
        UIManager.instance.livesText.text = "x " + currentLives;

        if (currentLives > 0)
        {
            //respawn code
            StartCoroutine(RespawnCo());

        }
        else
        {
            //game over code
            UIManager.instance.gameOverScreen.SetActive(true);
            WaveManager.instance.canSpawnWaves = false;

            MusicController.instance.PlayGameOver();
            PlayerPrefs.SetInt("HighScore", highScore);

            canPause = false;
        }
    }

    public IEnumerator RespawnCo()
    {

        yield return new WaitForSeconds(respawnTime);
        HealthManager.instance.Respawn();

        WaveManager.instance.ContinueSpawning();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        levelScore += scoreToAdd;
        UIManager.instance.scoreText.text = "Score: " + currentScore;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            UIManager.instance.hiScoreText.text = "Hi-Score: " + highScore;
            //PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public IEnumerator EndLevelCo()
    {
        UIManager.instance.levelEndScreen.SetActive(true);
        PlayerController.instance.StopMovement= true;
        levelEnding = true;
        MusicController.instance.PlayVictory();

        canPause = false;

        yield return new WaitForSeconds(.5f);

        UIManager.instance.endLevelScore.text = "Level Score: " + levelScore;
        UIManager.instance.endLevelScore.gameObject.SetActive(true);

        yield return new WaitForSeconds(.5f);

        PlayerPrefs.SetInt("CurrentScore", currentScore);
        UIManager.instance.endCurrentScore.text = "Total Score: " + currentScore;
        UIManager.instance.endCurrentScore.gameObject.SetActive(true);

        if (currentScore == highScore)
        {
            yield return new WaitForSeconds(.5f);
            UIManager.instance.highScoreNotice.SetActive(true);
        }

        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("CurrentLives", currentLives);

        yield return new WaitForSeconds(waitForLevelEnd);

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.StopMovement = false;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.instance.StopMovement = true;
        }
    }
}
