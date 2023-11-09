using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestCollectible : MonoBehaviour
{
    public int curValue = 0;
    private int minValue = 1, maxValue = 10;
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
        curValue = Random.Range(minValue, maxValue);
    }

    private void FixedUpdate()
    {
        canvasT?.LookAt(Camera.main.transform.position);
        if (progressActive)
        {
            progressTime += Time.deltaTime;
            slider.value = progressTime / timeToOpen;

            if(progressTime > timeToOpen && playOnce) // Open Chest 
            {
                playOnce = false;

                if (mandatory)
                {
                    CollectibleMaster.Instance.mandatoriesClaimed++;
                    CollectibleMaster.Instance.collectedValue += curValue;
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
            progressActive = true;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            progressActive = false;
        }
    }
}
