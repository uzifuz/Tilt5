using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public static AbilityController Instance;
    [SerializeField]
    public AbilitySlot AbilitySlot;

    [SerializeField]
    private Transform knight_attackPoint;
    [SerializeField]
    private float knight_attackRange = 1.0f;
    [SerializeField]
    private LayerMask knight_enemyLayer;

    private KnightAbility knightAbility;
    private ThiefAbility thiefAbility;
    private MageAbility mageAbility;

    [SerializeField]
    private List<Sprite> abilityIcons = new List<Sprite>();

    [SerializeField]
    private Animator animator;

    public Collider[] cols;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        knightAbility = new KnightAbility();
        thiefAbility = new ThiefAbility();
        mageAbility = new MageAbility();
        //SetAbilitySlotUI((CustomCharacterSettings.CharacterClass)PlayerPrefs.GetInt("playerClass"));
    }

    public void UseAbility()
    {
        if (!GameHandler.Instance.GameIsOver)
        {
            switch ((CustomCharacterSettings.CharacterClass)PlayerPrefs.GetInt("playerClass"))
            {
                case CustomCharacterSettings.CharacterClass.Knight:
                    knightAbility.UseAbility(knight_attackPoint, knight_attackRange, knight_enemyLayer);
                    break;
                case CustomCharacterSettings.CharacterClass.Thief:
                    thiefAbility.UseAbility();
                    break;
                case CustomCharacterSettings.CharacterClass.Wizard:
                    mageAbility.UseAbility();
                    break;
            }
        }
    }

    public void SetAbilitySlotUI(CustomCharacterSettings.CharacterClass characterClass)
    {
        switch (characterClass)
        {
            case CustomCharacterSettings.CharacterClass.Knight:
                AbilitySlot.AbilityIcon = abilityIcons[0];
                break;
            case CustomCharacterSettings.CharacterClass.Thief:
                AbilitySlot.AbilityIcon = abilityIcons[1];
                break;
            case CustomCharacterSettings.CharacterClass.Wizard:
                AbilitySlot.AbilityIcon = abilityIcons[2];
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(knight_attackPoint.position, knight_attackRange);
    }

    public void ActivateAttackCollider()
    {
        foreach(Collider col in cols)
        {
            col.enabled = true;
        }
    }

    public void DeactivateAttackCollider()
    {
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }
    }
}
