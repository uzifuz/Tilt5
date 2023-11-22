using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
   
    public GameObject LoadingScreenObject;
    public Slider LoadingBarFill;
    public TextMeshProUGUI LoadingText;

    //void Start()
    //{
    //    LoadingScreenObject.transform.LookAt(Camera.main.transform);
    //}
    public void Update()
    {
        LoadingBarFill.value = ProceduralLevelGenerator.Instance.RoomProgressValue;
        LoadingText.text = "Generating Level " + (ProceduralLevelGenerator.Instance.RoomProgressValue * 100).ToString("0.0") + "%";
        if (LoadingBarFill.value == 1)
        {
            LoadingScreenObject.SetActive(false);
        }
    }
}
