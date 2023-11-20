using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    public float moveSpeed = 3f, rotationMultiplier = 5f;
    public bool FollowPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowPlayer)
        {
            if(Thief.Instance != null)
            {
                transform.position = Thief.Instance.transform.position;
            }
        }
        else
        {
            transform.position += Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * moveSpeed + Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * moveSpeed;
        }

        /*if (Input.GetKey(KeyCode.Q))//TODO: Rotate with Tilt5 wand!!!
        {
            transform.localEulerAngles += new Vector3(0, -Time.deltaTime * moveSpeed * rotationMultiplier, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.localEulerAngles += new Vector3(0, Time.deltaTime * moveSpeed * rotationMultiplier, 0);
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))//Remove this on future releases!!!
        {
            Application.Quit();
        }
    }


}
