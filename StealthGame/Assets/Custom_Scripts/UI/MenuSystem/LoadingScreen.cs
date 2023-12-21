using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    public GameObject LoadingScreenObject;
    public Slider LoadingBarFill;
    [SerializeField] Image background;
    public TextMeshProUGUI LoadingText, DiffText;
    public float FadeTime = 3f;

    private void Start()
    {
        if(Instance == null)
            Instance = this;
        SetDifficultyText($"Level {PlayerPrefs.GetInt("DifficultyLevel")}");
    }

    public void Update()
    {
        LoadingBarFill.value = ProceduralLevelGenerator.Instance.RoomProgressValue;
        LoadingText.text = ProceduralLevelGenerator.Instance.RoomProgress;
        if (!LoadingScreenObject.activeSelf && background.color.a > 0f)
            background.CrossFadeAlpha(0f, FadeTime, false);
            //background.color = new Color(background.color.r, background.color.g, background.color.b, background.color.a - Time.deltaTime);
    }

    public void SetDifficultyText(string txt)
    {
        DiffText.text = txt;
    }

    public void Deactivate()
    {
        LoadingScreenObject.SetActive(false);
    }
}
