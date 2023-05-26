using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton Monobehaviour, use MenuHandler.Instance
/// </summary>
public class MenuHandler : MonoBehaviour
{
    public enum MenuType { Pause, Win, Lose}

    public static MenuHandler Instance { get; private set; }

    [SerializeField]
    GameObject[]
        PauseMenu,
        LoseMenu,
        WinMenu;


    private Dictionary<MenuType, GameObject[]> allMenus;

    void Start()
    {
        if(Instance != null)
        {
            Debug.LogError("multiple instances of MenuHandler in scene " + this.name);
        }
        Instance = this;

        allMenus = new()
        {
            { MenuType.Pause, PauseMenu },
            { MenuType.Lose, LoseMenu },
            { MenuType.Win, WinMenu }
        };
    }

    private void Update()
    {
        bool MenuIsOpen = PauseMenu[0].activeSelf || WinMenu[0].activeSelf || LoseMenu[0].activeSelf;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc pressed");
            if (PauseMenu[0].activeSelf)
            {
                ResumeGame();
            }
            else if(!MenuIsOpen)
            {
                PauseGame();
            }
        }
    }

    public void OpenMenu(MenuType menuType = MenuType.Pause)
    {
        //Time.timeScale = 0f;
        DeactivateAllMenus();
        switch(menuType)
        {
            case MenuType.Pause:
                SetEachObjectOfArray(PauseMenu, true);
                break;
            case MenuType.Win:
                SetEachObjectOfArray(WinMenu, true);
                break;
            case MenuType.Lose:
                SetEachObjectOfArray(LoseMenu, true);
                break;
        }
    }

    void SetEachObjectOfArray(GameObject[] newArray, bool newState)
    {
        for (int i = 0; i < newArray.Length; i++)
        {
            newArray[i].SetActive(newState);
        }
    }

    void DeactivateAllMenus()
    {
        SetEachObjectOfArray(PauseMenu, false);
        SetEachObjectOfArray(WinMenu, false);
        SetEachObjectOfArray(LoseMenu, false);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        DeactivateAllMenus();
    }

    public void ResumeGame()
    {
        Debug.Log("resuming game");
        CloseMenu();
        Time.timeScale = 1f;
    }

    private void PauseGame()
    {
        Debug.Log("Game Paused");
        OpenMenu(MenuType.Pause);
        Time.timeScale = 0f;
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("returning to menu");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void TryAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void NextLevel(int levelIndex)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
}
