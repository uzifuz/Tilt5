using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public void DebugButtonClick()
    {
        Debug.Log("Button was pressed");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.V))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("resuming game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        //Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    private void PauseGame()
    {
        Debug.Log("Game Paused");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        //Cursor.lockState = CursorLockMode.Confined;
        GameIsPaused = true;
    }

    public void ReturnToMenu()
    {
        Debug.Log("returning to menu");
        //pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        //Cursor.lockState = CursorLockMode.Locked; 
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
