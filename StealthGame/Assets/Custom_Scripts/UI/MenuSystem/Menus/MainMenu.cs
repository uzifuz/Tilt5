using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle gameModeToggle;

    [SerializeField]
    private GameObject tilt5Prototype;

    private void Start()
    {
        gameModeToggle.isOn = tilt5Prototype.activeSelf;
    }

    public void PlayGame(int sceneIndex)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void OnGameModeChanged()
    {
        bool tilt5 = gameModeToggle.isOn;
        Debug.Log(tilt5);
        tilt5Prototype.SetActive(tilt5);
        GetComponentInParent<RotateTowardsWand>().ShouldRotate = tilt5;
        
        if (tilt5)
        {
            PlayerPrefs.SetInt("Tilt5Mode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Tilt5Mode", 0);
            GetComponentInParent<RotateTowardsWand>().ResetRotation();
        }
    }
}
