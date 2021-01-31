﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectGiver : MonoBehaviour
{
    public int objectCheckIndex;
    private bool founded;
    public GameObject actionButton;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CanBeGrabbed")
        {
            if (other.gameObject.GetComponent<MeshCollider>() == null)
            {
                return;
            }
            if (GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().canWantObject() &&
                GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().wantedObject.GetComponent<MeshCollider>().sharedMesh ==
                other.gameObject.GetComponent<MeshCollider>().sharedMesh)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    print("match");
                    GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().objectTaked = true;
                    GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().LeaveQueue();
                    Invoke(nameof(DestroyObject), 1f);
                    actionButton.SetActive(false);
                    return;
                }
                actionButton.SetActive(true);
            }
            else
            {
                print("notMatch");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CanBeGrabbed")
        {
            if (other.gameObject.GetComponent<MeshCollider>() == null)
            {
                return;
            }
            if (GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().canWantObject() &&
                GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().wantedObject.GetComponent<MeshCollider>().sharedMesh ==
                other.gameObject.GetComponent<MeshCollider>().sharedMesh)
            {
                print("match");
                GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().objectTaked = true;
                Invoke(nameof(DestroyObject), 2);
            }
            else
            {
                print("notMatch");
            }
        }
    }

    void DestroyObject(Collider other)
    {
        Destroy(other.gameObject);
    }
}
