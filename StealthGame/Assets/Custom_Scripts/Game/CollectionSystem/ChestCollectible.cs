using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestCollectible : InteractableObject
{
    public int curValue = 0;
    public int minValue = 5, maxValue = 8;
    public bool mandatory = false;

    [SerializeField] Transform canvasT;
    [SerializeField] Slider slider;
    [SerializeField] BoxCollider boxCol;
    [SerializeField] Animator anim;

    public AudioClip clip;
    public float volume = 1;

    public float progressTime;
    public float timeToOpen = 5;

    public bool progressActive = false;
    private bool playOnce = true;

    private void OnEnable()
    {
        curValue = Random.Range(minValue, maxValue + PlayerPrefs.GetInt("DifficultyLevel")) * PlayerPrefs.GetInt("DifficultyLevel");
        InteractionText.text = "";
    }

    public override void ButtonHeldDown()
    {
        base.ButtonHeldDown();
        progressActive = true;
    }

    public override void ButtonUp()
    {
        base.ButtonUp();
        progressActive = false;
    }

    private void FixedUpdate()
    {
        if (progressActive)
        {
            if(progressTime < timeToOpen)
            {
                canvasT.gameObject.SetActive(true);
            }
            else
            {
                InteractionText.text = "";
                canvasT.gameObject.SetActive(false);
            }
            canvasT?.LookAt(Camera.main.transform.position);
            progressTime += Time.deltaTime;
            slider.value = progressTime / timeToOpen;

            if(progressTime > timeToOpen && playOnce) // Open Chest 
            {
                playOnce = false;

                if (mandatory)
                {
                    CollectibleMaster.Instance.mandatoriesClaimed++;
                    var obj = Instantiate(CollectibleMaster.Instance.messagePrefab, transform.position + Vector3.up, transform.rotation);
                    obj.GetComponent<CollectionMessage>().SetMessage("+" + curValue, Color.green);
                    PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + curValue);
                }
                CollectibleMaster.Instance.CheckCollection();

                anim.Play("Chest_Open");
                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
                boxCol.enabled = false;
                canvasT.gameObject.SetActive(false);
                progressActive = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            //progressActive = true;
            InteractionText.text = "Hold <b>1</b> To Open";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            //progressActive = false;
            InteractionText.text = "";
            canvasT.gameObject.SetActive(false);
        }
    }
}
