
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thief : ControllableEntity
{
    public static Thief Instance { get; private set; }

    public GameObject CharacterRenderer;
    CustomCharacterSettings characterSettings;
    float invisibilityDirection = -1f;
    float invisibility = 0f;
    [SerializeField] Slider healthSlider;

    public bool IsHidden
    {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Debug.LogWarning("more than one thief detected!");
            gameObject.SetActive(false);
        }
        IsHidden = false;
        characterSettings = GetComponentInChildren<CustomCharacterSettings>();
        Light[] lights = GetComponentsInChildren<Light>();
        foreach(Light light in lights)
        {
            light.intensity = 5f;
        }
        agentWalkSpeed = 1.5f * (100f + PlayerPrefs.GetFloat("SpeedMod", 1f)) / 100f;
        agentRunSpeed = 4f * (100f + PlayerPrefs.GetFloat("SpeedMod", 1f)) / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || TiltFiveInputs.Instance.trigger)
        {
            AbilityController.Instance.UseAbility();
        }
        //
        if(invisibility > 0f && invisibilityDirection < 0f)
        {
            invisibility -= Time.deltaTime;
            SetInvisibility();
        }
        else if(invisibility < 1f && invisibilityDirection > 0f)
        {
            invisibility += Time.deltaTime;
            SetInvisibility();
        }
        if(IsHidden)
        {
            healthSlider.gameObject.SetActive(false);
        }
        else
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = CurHealth;
        }
    }

    public void KnightAbility()
    {
        anim.SetFloat("animSpeed", 1);
        anim.Play("Ability_Knight", 1);
    }

    public void ThiefAbility()
    {
        invisibilityDirection = -invisibilityDirection;
    }

    public void WizardAbility()
    {
        anim.SetFloat("animSpeed", 1);
        anim.Play("Ability_Wizard", 1);
    }

    void SetInvisibility()
    {
        foreach (Renderer ren in characterSettings.AllRenderer)
        {
            ren.material.SetFloat("_Invisibility", invisibility);
        }
        if (invisibility >= 0.7f)
        {
            IsHidden = true;
        }
        else
        {
            IsHidden = false;
        }
    }
}
