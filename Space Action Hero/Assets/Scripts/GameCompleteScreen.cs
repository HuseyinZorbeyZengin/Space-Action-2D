using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompleteScreen : MonoBehaviour
{
    public float timeBetweenTexts;

    public bool canExit;

    public string mainMenuName = "MainMenu";

    public Text message, score, pressKey;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowTextCo());
    }

    // Update is called once per frame
    void Update()
    {
        if (canExit && Input.anyKeyDown)
        {
            SceneManager.LoadScene(mainMenuName);
        }
    }


    public IEnumerator ShowTextCo()
    {
        yield return new WaitForSeconds(timeBetweenTexts);
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        score.text = "Final Score: " + PlayerPrefs.GetInt("CurrentScore");
        score.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        pressKey.gameObject.SetActive(true);
        canExit = true;
    }
}
