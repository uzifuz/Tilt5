using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    public static bool Instance { get; private set; }
    private static bool _thiefDetected;
    public static bool ThiefDetected
    {
        get { return _thiefDetected; }
        set
        {
            _thiefDetected = value;

            if (value == true)
            {
                Debug.Log("Thief Detected!");
                //todo: start alarm
            } else
            {
                //todo: stop alarm
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance)
        {
            Debug.LogWarning("more than one instance of Detectionhanddler");
            gameObject.SetActive(false);
        } else
        {
            Instance = this;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
