using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    string entityName;

    // Start is called before the first frame update
    void Start()
    {
        InheritStart();
    }

    // Update is called once per frame
    void Update()
    {
        InheritUpdate();
    }

    protected virtual void InheritStart()
    {

    }

    protected virtual void InheritUpdate()
    {

    }
}
