using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool tilt5;
    [SerializeField] GameObject tilt5Prototype;

    [SerializeField] Transform cameraDirectionTransform;

    public float CamMoveSpeed = 3f, CamTurnSpeed = 90f;

    private void Start()
    {
        tilt5 = PlayerPrefs.GetInt("Tilt5Mode") != 1;
        OnGameModeChanged();
    }

    public void ChangeCameraView(Transform newViewTarget)
    {
        cameraDirectionTransform = newViewTarget;
    }

    public void PlayGame(int sceneIndex)
    {
        PlayerPrefs.SetInt("DifficultyLevel", 1);
        PlayerPrefs.SetInt("CurrentScore", 0);
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
        tilt5 = !tilt5;
        tilt5Prototype.SetActive(tilt5);
        if (tilt5)
        {
            PlayerPrefs.SetInt("Tilt5Mode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Tilt5Mode", 0);
        }
    }
}
