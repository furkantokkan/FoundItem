using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    private List<GameObject> shelfList = new List<GameObject>();
    public List<GameObject> emptyShelfList = new List<GameObject>();

    private void Start()
    {
        shelfList.AddRange(GameObject.FindGameObjectsWithTag("Shelf"));
    }
    private void Update()
    {
        for (int i = 0; i < shelfList.Count; i++)
        {
            if (!emptyShelfList.Contains(shelfList[i].gameObject))
            {
                if (shelfList[i].GetComponent<Shelf>().HaveEmptyPlace())
                {
                    emptyShelfList.Add(shelfList[i].gameObject);
                }
            }
        }
    }
}
