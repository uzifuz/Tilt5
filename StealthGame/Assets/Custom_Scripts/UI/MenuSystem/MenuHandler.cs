using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Singleton Monobehaviour, use MenuHandler.Instance
/// </summary>
public class MenuHandler : MonoBehaviour
{
    public enum MenuType { Pause, Win, Lose}
    ClickToMoveEntity moveController;
    public static MenuHandler Instance { get; private set; }
    [SerializeField] TextMeshProUGUI[] scoreTexts;

    [SerializeField]
    GameObject[]
        PauseMenu,
        LoseMenu,
        WinMenu;


    private Dictionary<MenuType, GameObject[]> allMenus;
    int DifficultySave;

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

        moveController = FindObjectOfType<ClickToMoveEntity>();
        DifficultySave = PlayerPrefs.GetInt("DifficultyLevel");
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
        //Nothing moving logic needs to be here!!!
        TiltFiveBoardMover.Instance.boardLocked = true;
        Debug.Log($"Cur Score {PlayerPrefs.GetInt("CurrentScore")}");
        string highscoreNote = CheckForNewHighscore() ? "<b>Highscore</b>" : "<b>Current Score</b>";
        foreach (TextMeshProUGUI textM in scoreTexts)
        {
            textM.text = $"{highscoreNote}: {PlayerPrefs.GetInt("CurrentScore")}";
        }
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
        TiltFiveBoardMover.Instance.boardLocked = false;
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
        DetectionHandler.Instance.ThiefDetected = false;
        GameHandler.Instance.GameOutComeReset();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void TryAgain()
    {
        Time.timeScale = 1.0f;
        DetectionHandler.Instance.ThiefDetected = false;
        GameHandler.Instance.GameOutComeReset();
        SceneManager.LoadScene(1);
    }

    public void NextLevel(int levelIndex)
    {
        Time.timeScale = 1.0f;
        PlayerPrefs.SetInt("DifficultyLevel", DifficultySave + 1);
        DetectionHandler.Instance.ThiefDetected = false;
        GameHandler.Instance.GameOutComeReset();
        SceneManager.LoadScene(1);
    }

    public bool CheckForNewHighscore()
    {
        if(PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", PlayerPrefs.GetInt("CurrentScore"));
            return true;
        }
        return false;
    }
}
