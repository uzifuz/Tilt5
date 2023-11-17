using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityController : MonoBehaviour
{

    [SerializeField]
    public AbilitySlot AbilitySlot;

    [SerializeField]
    private Transform knight_attackPoint;
    [SerializeField]
    private float knight_attackRange = 1.0f;
    [SerializeField]
    private LayerMask knight_enemyLayer;

    private KnightAbility knightAbility = new KnightAbility();
    private ThiefAbility thiefAbility = new ThiefAbility();
    private MageAbility mageAbility = new MageAbility();

    [SerializeField]
    private List<Sprite> abilityIcons = new List<Sprite>();

    [SerializeField]
    private Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (!GameHandler.Instance.GameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch(Thief.Instance.CharacterClass)
                {
                    case CharacterClass.Knight:
                        knightAbility.UseAbility(animator, knight_attackPoint, knight_attackRange, knight_enemyLayer);
                        break;
                    case CharacterClass.Thief:
                        thiefAbility.UseAbility();
                        break;
                    case CharacterClass.Mage:
                        mageAbility.UseAbility();
                        break;
                }
            }
        }
    }

    public void SetAbilitySlotUI(CharacterClass characterClass)
    {
        switch (characterClass)
        {
            case CharacterClass.Knight:
                AbilitySlot.AbilityIcon = abilityIcons[0];
                break;
            case CharacterClass.Thief:
                AbilitySlot.AbilityIcon = abilityIcons[1];
                break;
            case CharacterClass.Mage:
                AbilitySlot.AbilityIcon = abilityIcons[2];
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(knight_attackPoint.position, knight_attackRange);
    }
}
