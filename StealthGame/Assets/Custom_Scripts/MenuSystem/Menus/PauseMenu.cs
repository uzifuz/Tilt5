using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void DebugButtonClick()
    {
        Debug.Log("Button was pressed");
    }

    public void Resume()
    {
        MenuHandler.Instance.ResumeGame();
    }

    public void ReturnToMenu()
    {
        Debug.Log("returning to menu");
        //pauseMenuUI.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked; 
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
