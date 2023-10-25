using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitPointer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform thiefPos;
    public Transform winPos;
    public Transform cameraPos;

    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = winPos.position - thiefPos.position;
        direction.Normalize();


        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, -angle + 90 + cameraPos.rotation.y);

    }

    public void UpdateRotation()
    {
        gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}
