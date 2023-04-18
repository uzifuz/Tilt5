using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    [SerializeField] Transform lookTarget;
    public Vector3 localScaleMod;

    private void Update()
    {
        transform.LookAt(lookTarget.position);
        transform.localScale = localScaleMod;
    }
}
