using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Singleton, use GameHandler.Instance
/// </summary>
public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    public enum GameOutcome { ThiefWin, ThiefLose }
    public bool GameIsOver { get; private set; }


    [SerializeField]
    private GameObject tilt5Prototype;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        FindObjectOfType<ProceduralLevelGenerator>().StartCoroutine("RoomGenCo");
        if(PlayerPrefs.GetInt("Tilt5Mode") == 1)
        {
            Debug.Log("Tilt5 mode activated");
            tilt5Prototype.SetActive(true);
        }
        else
        {
            Debug.Log("Normie mode activated");
            tilt5Prototype.SetActive(false);
        }
    }

    public void GameOver(GameOutcome outcome)
    {
        GameIsOver = true;
        Debug.Log("Game is Over");
        Thief.Instance.CanMove = false;
        switch (outcome)
        {
            case GameOutcome.ThiefWin:
                DetectionHandler.Instance.ThiefDetected = false;

                MenuHandler.Instance.OpenMenu(MenuHandler.MenuType.Win);
                break;
            case GameOutcome.ThiefLose:
            default:
                MenuHandler.Instance.OpenMenu(MenuHandler.MenuType.Lose);
                break;

        }
    }
    public void GameOutComeReset()
    {
        GameIsOver = false;
    }
}
