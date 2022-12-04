using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenu()
    {
        Debug.Log("returning to main menu");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
