using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Toggle gameModeToggle;

    [SerializeField] GameObject tilt5Prototype;

    [SerializeField] Transform cameraDirectionTransform;

    public float CamMoveSpeed = 3f, CamTurnSpeed = 90f;

    private void Start()
    {
        gameModeToggle.isOn = tilt5Prototype.activeSelf;
        OnGameModeChanged();
    }

    private void Update()
    {
        CameraToMatchViewDirection();
    }

    public void ChangeCameraView(Transform newViewTarget)
    {
        cameraDirectionTransform = newViewTarget;
    }

    void CameraToMatchViewDirection()
    {
        if(Camera.main.transform.position != cameraDirectionTransform.position)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraDirectionTransform.position, CamMoveSpeed);
        }

        if(Camera.main.transform.eulerAngles.y != cameraDirectionTransform.eulerAngles.y)
        {
            Camera.main.transform.eulerAngles += Time.deltaTime * CamTurnSpeed * Vector3.up * 
                (Camera.main.transform.eulerAngles.y > cameraDirectionTransform.eulerAngles.y ? Camera.main.transform.eulerAngles.y - cameraDirectionTransform.eulerAngles.y : cameraDirectionTransform.eulerAngles.y - Camera.main.transform.eulerAngles.y);
        }
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
        tilt5Prototype.SetActive(tilt5);
        GetComponentInParent<RotateTowardsWand>().ShouldRotate = tilt5;
        
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
