using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAlarm : MonoBehaviour
{
    public static DetectionAlarm Instance;
    [SerializeField]
    float alarmDuration = 10f;
    float alarmTimer;
    bool alarmEnabled = false;
    DetectionUI[] detectionUI;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        detectionUI = FindObjectsOfType<DetectionUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alarmEnabled)
        {
            UpdateTimer();
            if(alarmTimer <= 0)
            {
                alarmEnabled = false;
                alarmTimer = -1;
                GameHandler.Instance.GameOver(GameHandler.GameOutcome.ThiefLose);
            }
        }
    }

    public void StartAlarmCountDown()
    {

        if(!alarmEnabled)
        {
            Debug.Log("Starting alarm");
            alarmTimer = alarmDuration;
            alarmEnabled = true;
        }
    }

    private void UpdateTimer()
    {
        alarmTimer -= Time.deltaTime;
        foreach(DetectionUI det in detectionUI)
        {
            det.UpdateText(alarmTimer);
        }
    }
}
