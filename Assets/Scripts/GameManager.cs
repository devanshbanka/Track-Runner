using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isAlive = true;

    public static GameManager MyInstance;

    public GameObject ScoreObj;
    public TextMeshProUGUI ScoreText;
    public int Score;

    public GameObject startTimerObj;
    public TextMeshProUGUI startTimerText;
    public float timeLeft;

    public GameObject PauseButton;
    public GameObject PausedPanel;

    public GameObject GameoverPanel;
    public GameObject StartGamePanel;

    private void Awake()
    {
        MyInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartGamePanel.SetActive(true);
        GameoverPanel.SetActive(false);
        startTimerObj.SetActive(false);
        PauseButton.SetActive(false);
        PausedPanel.SetActive(false);
        ScoreObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString();

        if (startTimerObj.activeSelf)
        {
            timeLeft -= Time.unscaledDeltaTime;
            startTimerText.text = (timeLeft).ToString("0");
            if (timeLeft < 1)
            {
                Time.timeScale = 1;
                startTimerObj.SetActive(false);
            }
        }
    }

    public IEnumerator Dead()
    {
        Time.timeScale = 0;
        isAlive = false;

        float startTime = Time.unscaledTime;
        float waitTime = 1f;

        PauseButton.SetActive(false);

        while (Time.unscaledTime - startTime < waitTime)
        {
            yield return null;
        }

        GameoverPanel.SetActive(true);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        StartGamePanel.SetActive(false);
        startTimerObj.SetActive(true);
        PauseButton.SetActive(true);
        ScoreObj.SetActive(true);
    }

    public void PauseGame()
    {
        PauseButton.SetActive(false);
        Time.timeScale = 0;
        PausedPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        PauseButton.SetActive(true);
        PausedPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
