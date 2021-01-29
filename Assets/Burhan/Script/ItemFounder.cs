using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFounder : MonoBehaviour
{
    private List<Transform> items = new List<Transform>();


    public Transform ClosestItem()
    {
        items.Clear();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            items.Add(item.transform);
        }

        return GetClosestItem(items);
    }

    Transform GetClosestItem(List<Transform> items)
    {
        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform potentialTarget in items)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistance)
            {
                closestDistance = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

}
