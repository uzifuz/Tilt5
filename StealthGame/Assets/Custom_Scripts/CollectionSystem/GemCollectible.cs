using System;
using UnityEngine;

public class GemCollectible : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 1;
    public bool mandatory = false;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            if(mandatory)
            {
                CollectibleMaster.Instance.mandatoriesClaimed++;
            }
            CollectibleMaster.Instance.CheckCollection();
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Destroy(gameObject);
        }
    }
}
