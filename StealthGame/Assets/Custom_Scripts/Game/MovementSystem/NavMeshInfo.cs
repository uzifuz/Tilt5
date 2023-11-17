using UnityEngine;
using UnityEngine.AI;

public class NavMeshInfo : MonoBehaviour
{
    public static Vector3 RandomNavSphere(Vector3 origin, float minDistance, float maxDistance, int layermask)
    {
        if(minDistance > maxDistance)
        {
            minDistance = maxDistance;
        }
        float distance = Random.Range(minDistance, maxDistance);
        if(distance <= 0)
        {
            distance = 1;
        }
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public static bool IsDestinationOnNavMesh(Vector3 destination)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(destination, out hit, 1f, NavMesh.AllAreas);
    }
}
