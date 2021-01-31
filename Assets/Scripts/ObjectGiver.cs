using System.Collections;
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
                print("match");
                GameManager.instance.queue[objectCheckIndex].clientList[0].GetComponent<AI>().objectTaked = true;
                Destroy(other.gameObject, 1f);
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
                Destroy(other.gameObject, 1f);
            }
            else
            {
                print("notMatch");
            }
        }
    }


}
