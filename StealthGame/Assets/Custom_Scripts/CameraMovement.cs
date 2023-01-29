using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 3f, rotationMultiplier = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * moveSpeed + Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * moveSpeed;
        if(Input.GetKey(KeyCode.Q))
        {
            transform.localEulerAngles += new Vector3(0, -Time.deltaTime * moveSpeed * rotationMultiplier, 0);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            transform.localEulerAngles += new Vector3(0, Time.deltaTime * moveSpeed * rotationMultiplier, 0);
        }
        if(Input.GetKeyDown(KeyCode.Escape))//Remove this on future releases!!!
        {
            Application.Quit();
        }
    }


}
