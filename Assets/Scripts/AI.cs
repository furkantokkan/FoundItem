using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;
public class AI : MonoBehaviour
{
    [Header("Move Settings")]
    public float agentOffset = 1f;
    [Header("Object Pick Settings")]
    public GameObject wantedObject;
    public GameObject previewParrent;

    NavMeshAgent agent;
    Transform target;
    internal int indexCount;
    internal bool objectTaked = false;
    private bool reachedFirstPos = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        GameManager.instance.queue[indexCount].clientList.Add(this.gameObject);
        target = RequestNewTarget();
        wantedObject = GameManager.instance.GetRandomObject();
    }

    void Update()
    {

        if (canWantObject())
        {
            previewParrent.SetActive(true);
            previewParrent.GetComponentInChildren<Image>().sprite = wantedObject.GetComponent<ObjectProperties>().previewImage;
        }
        else
        {
            previewParrent.SetActive(false);
        }

        if (target)
        {
            if (reachedFirstPos)
            {
                float targetDirection = Vector3.Distance(transform.position, target.transform.position);
                agent.SetDestination(target.transform.position);
                if (targetDirection < agentOffset)
                {
                    agent.SetDestination(transform.position);
                }
            }
            else
            {
                Vector3 newPos = new Vector3(GameManager.instance.queue[indexCount].headOfQueue.position.x, transform.position.y, target.transform.position.z);
                float newPosDirection = Vector3.Distance(transform.position, newPos);
                agent.SetDestination(newPos);
                if (newPosDirection < agentOffset)
                {
                    reachedFirstPos = true;
                }
            }
        }
        else
        {
            target = RequestNewTarget();
        }
    }
   public bool canWantObject()
    {
        if (transform.position.x >= GameManager.instance.queue[indexCount].headOfQueue.transform.position.x
          && transform.position.z >= GameManager.instance.queue[indexCount].headOfQueue.transform.position.z &&
          this.gameObject == GameManager.instance.queue[indexCount].clientList[0].gameObject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDisable()
    {
        GameManager.instance.queue[indexCount].clientList.Remove(this.gameObject);
        GameManager.instance.queue[indexCount].currentClientCount--;
    }
    Transform RequestNewTarget()
    {
        if (this.gameObject == GameManager.instance.queue[indexCount].clientList[0].gameObject)
        {
            agentOffset = 0.1f;
            return GameManager.instance.queue[indexCount].headOfQueue.transform;
        }
        else
        {
            Transform newTarget = GameManager.instance.queue[indexCount].clientList[GameManager.instance.queue[indexCount].currentClientCount - 2].transform;
            return newTarget;
        }
    }

}
