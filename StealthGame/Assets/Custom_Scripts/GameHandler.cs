using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton, use GameHandler.Instance
/// </summary>
public class GameHandler
{
    public enum GameOutcome { ThiefWin, ThiefLose }
    private static GameHandler? _instance;
    public static GameHandler Instance
    {
        get
        {
            _instance ??= new GameHandler();
            return _instance;
        }
    }
    public bool GameIsOver { get; private set; }
    private GameHandler()
    {

    }
    public void GameOver(GameOutcome outcome)
    {
        GameIsOver = true;
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
}
