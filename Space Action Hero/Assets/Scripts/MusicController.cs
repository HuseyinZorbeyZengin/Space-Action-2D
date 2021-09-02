using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioSource levelMusic, bossMusic, victoryMusic, gameOverMusic;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        levelMusic.Play();
    }


    void Update()
    {

    }


    void StopMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        gameOverMusic.Stop();
    }

    public void PlayBoss()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void PlayVictory()
    {
        StopMusic();
        victoryMusic.Play();
    }
    public void PlayGameOver()
    {
        StopMusic();
        gameOverMusic.Play();
    }
}
