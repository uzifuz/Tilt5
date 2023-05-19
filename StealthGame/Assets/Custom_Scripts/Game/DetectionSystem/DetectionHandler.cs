using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    public static DetectionHandler Instance;
    public bool ThiefDetected;
    [SerializeField]
    GameObject GameOverMenuPrefab;
    GameObject GameOverMenu;

    void Start()
    {
        Instance = this;
        GameOverMenu = Object.Instantiate(GameOverMenuPrefab);
        ThiefDetected = false;
    }

    void Update()
    {
        if (ThiefDetected && !GameHandler.Instance.GameIsOver)
        {
           DetectionAlarm.Instance.StartAlarmCountDown();
        }
    }
}
