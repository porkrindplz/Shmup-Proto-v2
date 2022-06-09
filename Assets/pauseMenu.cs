using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool isGameOver = false;
    public GameObject pauseMenuUI;
    public GameObject gameoverMenuUI;
    public GameObject playUI;
    public static bool _levelComplete = false;
    public GameObject _levelCompleteMenu;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (_levelComplete)
        {
            Complete();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        _levelCompleteMenu.SetActive(false);
        Time.timeScale = 1f;
        playerController.isPlaying = true;
        GameIsPaused = false;
        isGameOver = false;
        _levelComplete = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverMenuUI.SetActive(true);
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        PlayerController.restartCount++;
        SceneManager.LoadScene(1);
        Resume();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        PlayerController.restartCount = 0;
        SceneManager.LoadScene(0);
        isGameOver = false;
        _levelComplete = false;
      
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Complete()
    {
        _levelCompleteMenu.SetActive(true);
        playUI.SetActive(false);
        playerController.isPlaying = false;
    }

}
