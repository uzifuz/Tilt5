using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestCollectible : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            Debug.Log("Collision");
            GetComponent<Animator>().Play("Chest_Open");
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(WaitForAnimationToFinish());
        }
    }

    IEnumerator WaitForAnimationToFinish()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }

}
