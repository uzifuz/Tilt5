using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestCollectible : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 1;
    public float progressTime;
    public float timeToOpen = 5;
    public bool progressActive = false;
    private bool playOnce = true;

    private void FixedUpdate()
    {
        GetComponentInChildren<Canvas>().transform.LookAt(Camera.main.transform.position);
        if (progressActive)
        {
            progressTime += Time.deltaTime;
            GetComponentInChildren<Slider>().value = progressTime / timeToOpen;

            if(progressTime > timeToOpen && playOnce)
            {
                playOnce = false;
                GetComponent<Animator>().Play("Chest_Open");
                AudioSource.PlayClipAtPoint(clip, transform.position, volume);
                GetComponent<BoxCollider>().enabled = false;
                GetComponentInChildren<Canvas>().enabled = false;
                progressActive = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            progressActive = true;
            FixedUpdate();
            Debug.Log("Collision");
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
