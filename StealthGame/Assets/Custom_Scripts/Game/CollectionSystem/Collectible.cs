using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int curValue = 0;
    [SerializeField] int minValue = 1, maxValue = 100;
    public AudioClip clip;
    public float volume = 1;
    public bool mandatory = false;

    private void OnEnable()
    {
        curValue = Random.Range(minValue, maxValue);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, Time.time * 100f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thief"))
        {
            if (mandatory)
            {
                CollectibleMaster.Instance.mandatoriesClaimed++;
                CollectibleMaster.Instance.collectedValue += (int)(curValue * CollectibleMaster.Instance.valueMultiplier);
            }
            CollectibleMaster.Instance.CheckCollection();
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Destroy(gameObject);
        }
    }
}
