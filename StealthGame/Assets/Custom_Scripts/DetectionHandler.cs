using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    public static DetectionHandler Instance { get; private set; }
    private bool _thiefDetected;
    public static bool ThiefDetected
    {
        get { return Instance._thiefDetected; }
        set
        {
            //if already detected, don't start alarm again
            if (!ThiefDetected || value == false) Instance.SetAlarm(value);
            Instance._thiefDetected = value;
        }
    }

    [SerializeField]
    TMP_Text AlarmDisplay;
    [SerializeField]
    float alarmTime;
    float alarmTimer;
    Coroutine alarmCoroutine;
    void Start()
    {
        Instance = this;
        ThiefDetected = false;
    }
    private void SetAlarm(bool state)
    {
        if (state == true)
        {
            AlarmDisplay.gameObject.SetActive(true);
            alarmCoroutine ??= StartCoroutine(nameof(UpdateTimer));

        } else
        {
            AlarmDisplay.gameObject.SetActive(false);
            if (alarmCoroutine != null) StopCoroutine(alarmCoroutine);
            AlarmDisplay.text = "";
        }
    }
    private IEnumerator UpdateTimer()
    {
        alarmTimer = alarmTime;
        for (alarmTimer = alarmTime; alarmTimer > 0; alarmTimer -= Time.deltaTime)
        {
            int decimalPlaces = 2;
            float pow = Mathf.Pow(10, decimalPlaces);
            string roundedTimer = (Mathf.Round(alarmTimer * pow) / pow).ToString();
            while (roundedTimer.Count() < 4) roundedTimer += (roundedTimer.Count() < 2) ? "." : "0";
            AlarmDisplay.text = $"DETECTED\n" +
                    "police arriving in: " + roundedTimer;
            yield return new WaitForSeconds(Time.deltaTime);
            alarmTimer -= Time.deltaTime;
        }

        //todo: GameOver game logic
        AlarmDisplay.text = "GAME OVER";
    }
}