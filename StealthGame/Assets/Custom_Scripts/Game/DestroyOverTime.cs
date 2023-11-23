using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;

    private void Awake()
    {
        StartCoroutine(LifeCo());
    }

    IEnumerator LifeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
