using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionMessage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI msgText;
    [SerializeField] RotateTowardsWand rotater;
    [SerializeField] float lifeTime = 3f;
    bool flashing = false;
    float flashDirection = 1f;
    Color refColor = Color.white;

    private void Start()
    {
        rotater = GetComponent<RotateTowardsWand>();
        if(PlayerPrefs.GetInt("Tilt5Mode") == 0)
        {
            rotater.wandPosition = Camera.main.transform;
        }
        else
        {
            rotater.wandPosition = GameObject.FindGameObjectWithTag("Wand").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float bChannel = refColor.b;
        if(flashing)
        {
            if (flashDirection > 0f)
            {
                flashDirection -= Time.deltaTime;
            }
            bChannel += flashDirection * Time.deltaTime * 2f;
            if (bChannel > 1 || bChannel < 0)
                flashDirection = -flashDirection;
        }
        refColor = new Color(refColor.r, refColor.g, bChannel, lifeTime);
        msgText.color = refColor;
        transform.position += Vector3.up * Time.deltaTime * 2f;
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
    }

    public void SetMessage(string newMessage, Color newColor, bool setFlashing = false)
    {
        msgText.text = newMessage;
        flashing = setFlashing;
        refColor = newColor;
    }
}
