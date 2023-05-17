using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Singleton Monobehaviour, use MenuHandler.Instance
/// </summary>
public class MenuHandler : MonoBehaviour
{
    public enum MenuType { Pause, Win, Lose}

    public static MenuHandler Instance { get; private set; }

    [SerializeField]
    GameObject
        PauseMenu,
        LoseMenu,
        WinMenu;

    public bool MenuIsOpen 
        => allMenus
        .Where(
            predicate: (KeyValuePair<MenuType, GameObject> menuPair) => menuPair.Value.activeSelf)
        .Any();


    private Dictionary<MenuType, GameObject> allMenus;

    void Start()
    {
        if(Instance != null)
        {
            Debug.LogError("multiple instances of MenuMaster in scene");
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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("Esc or V pressed");
            if (MenuIsOpen && allMenus[MenuType.Pause].activeSelf)
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
        foreach(var pair in allMenus)
        {
            var menuObject = pair.Value;
            if (menuObject.activeSelf)
            {
                menuObject.SetActive(false);
            }
        }
        allMenus[menuType].SetActive(true);
    }

    public void CloseMenu()
    {
        foreach (var pair in allMenus)
        {
            var menuObject = pair.Value;
            menuObject.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Debug.Log("resuming game");
        CloseMenu();
    }

    private void PauseGame()
    {
        Debug.Log("Game Paused");
        OpenMenu(MenuType.Pause);
    }
}
