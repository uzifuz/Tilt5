using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    public int count;
    public static CollectibleCount Instance { get; private set; }
    public static bool winCondition = false;

    void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    void Start() => UpdateCount();

    void OnEnable() {
        GemCollectible.OnCollected += OnCollectibleCollected;
        MoneyCollectible.OnCollected += MoneyCollected;
    }

    void OnDisable()
    {
        GemCollectible.OnCollected -= OnCollectibleCollected;
        MoneyCollectible.OnCollected -= MoneyCollected;
    }

    void MoneyCollected()
    {
        MoneyCollectible.moneyCount += 100;
        UpdateCount();
    }

    void OnCollectibleCollected()
    {
        count++;
        UpdateCount();
    }

    void UpdateCount()
    {
 
        if(count >= GemCollectible.totalCount)
        {
            text.text = $"WinCondition\nMoney: { MoneyCollectible.moneyCount}";
            winCondition = true;
            GemCollectible.totalCount = -1;
        }
        else if(GemCollectible.totalCount != -1)
        {
            text.text = $"{count} / {GemCollectible.totalCount}\nMoney: { MoneyCollectible.moneyCount}";
        }
        else
        {
            text.text = $"\nMoney: { MoneyCollectible.moneyCount}";
        }
    }
}
