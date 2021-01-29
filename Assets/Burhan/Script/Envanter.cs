using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envanter : MonoBehaviour
{
    public GameObject itemImage;
    public int envanterYuvaSayisi;

    public List<GameObject> elimizdekiler = new List<GameObject>();
    private GameObject itemHand;

    Transform envanterTransform;
    ItemFounder founder;

    void Start()
    {
        founder = GetComponent<ItemFounder>();
        envanterTransform = GameObject.FindGameObjectWithTag("Envanter").transform;

        EnvanterSocketCount();

        elimizdekiler.Clear();
    }

    private void EnvanterSocketCount()
    {
        for (int i = 0; i < envanterYuvaSayisi; i++)
        {
            Instantiate(itemImage, envanterTransform.transform.position, Quaternion.identity, envanterTransform);
        }

        RectTransform rect = envanterTransform.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(150 * envanterYuvaSayisi + 50, rect.sizeDelta.y);

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetItem();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            EldekiniTeslimEt();
        }


        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ReleaseItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ElineAl(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ElineAl(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ElineAl(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ElineAl(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ElineAl(5);
        }

    }

    private void ElineAl(int girdi)
    {
        if(girdi <= envanterYuvaSayisi)
        {
            if(girdi <= elimizdekiler.Count)
            {
                TakeItem(girdi);
            }
            else
            {
                ReleaseItem();
            }
        }
    }

    private void TakeItem(int itemNumber)
    {
        itemHand = elimizdekiler[itemNumber];
        Debug.Log(elimizdekiler[itemNumber].name + " adlı item ele alındı");
    }

    private void ReleaseItem()
    {
        Debug.Log("Elindekini bıraktı");
    }

    private void EldekiniTeslimEt()
    {
        Debug.Log("Eldeki eşya teslim etme fankçını");
    }
   

    private void GetItem()
    {
        if (elimizdekiler.Count < envanterYuvaSayisi)
        {
            elimizdekiler.Add(founder.ClosestItem().gameObject);
        }
        else
        {
            Debug.Log("Envanter is full.");
        }
    }



}
