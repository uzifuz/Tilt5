using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsWand : MonoBehaviour
{
    public Transform wandPosition;
    public float turnSpeed = 30f;
    public Vector3 defaultRotation;
    public bool ShouldRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldRotate && wandPosition != null)
        {
            // Determine which direction to rotate towards
            Vector3 targetPosition = new Vector3(wandPosition.position.x, transform.position.y, wandPosition.position.z);
            Vector3 targetDirection = targetPosition - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = turnSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if(wandPosition == null)
        {
            wandPosition = Camera.main.transform;
        }
    }

    public void ResetRotation()
    {
        transform.localEulerAngles = defaultRotation;
    }
}
