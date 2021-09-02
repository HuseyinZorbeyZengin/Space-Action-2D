using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string firstLevel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Startgame()
    {

        PlayerPrefs.SetInt("CurrentLives", 3);
        PlayerPrefs.SetInt("CurrentScore", 0);

        SceneManager.LoadScene(firstLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
