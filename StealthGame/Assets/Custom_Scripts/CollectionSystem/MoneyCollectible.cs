using System;
using UnityEngine;

public class MoneyCollectible : MonoBehaviour
{
    public static event Action OnCollected;
    public static int moneyCount = 0;
    public AudioClip clip;
    public float volume = 1;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Thief"))
        {
            Debug.Log("Got Money");
            OnCollected?.Invoke();
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Destroy(gameObject);
        }
    }
}
