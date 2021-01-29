using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private List<GameObject> childObjectsList = new List<GameObject>();
    public List<GameObject> emptyPlaces = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjectsList.Add(transform.GetChild(i).gameObject);
        }
    }
    public List<GameObject> RequestNewEmptyPlaces()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (childObjectsList[i].transform.childCount == 0)
            {
                if (!emptyPlaces.Contains(childObjectsList[i].gameObject))
                {
                    emptyPlaces.Add(childObjectsList[i].gameObject);
                }
            }
        }
        return emptyPlaces;
    }
    public bool HaveEmptyPlace()
    {
        if (RequestNewEmptyPlaces().Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
