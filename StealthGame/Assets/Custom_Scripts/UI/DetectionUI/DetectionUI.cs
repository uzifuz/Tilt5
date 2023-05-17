using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectionUI : MonoBehaviour
{
    TextMeshProUGUI detectionText;

    // Start is called before the first frame update
    void Start()
    {
        detectionText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(float remainingTime)
    {
        if (detectionText != null)
        {
            string timeText = remainingTime.ToString("0.00");
            detectionText.text = $"DETECTED\n{timeText}";
        }
    }
}
