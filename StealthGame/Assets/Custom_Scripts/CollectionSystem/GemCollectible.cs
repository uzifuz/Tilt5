using System;
using UnityEngine;

public class GemCollectible : MonoBehaviour
{
    public static event Action OnCollected;
    public static int totalCount = 0;
    public AudioClip clip;
    public float volume = 1;

    void Awake() => totalCount++;

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Thief"))
        {
            Debug.Log("Collision");
            OnCollected?.Invoke();
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Destroy(gameObject);
        }
    }
}
