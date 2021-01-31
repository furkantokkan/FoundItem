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
    internal Animator anim;
    internal int indexCount;
    internal bool objectTaked = false;
    private bool reachedFirstPos = false;
    private bool leave = true;
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
        Vector3 velo = agent.velocity;
        Vector3 localvel = transform.InverseTransformDirection(velo);
        if (localvel.z < 0.1)
        {
            anim.SetInteger("State", 3);
        }
        else
        {
            anim.SetInteger("State", 1);
        }
        if (objectTaked)
        {
            try
            {
                Transform exitPos = GameManager.instance.exit.transform;
                float distance = Vector3.Distance(transform.position, exitPos.position);
                if (leave)
                {
                    LeaveQueue();
                    print("leave");
                    leave = false;
                }
                if (distance < 2f)
                {
                    Destroy(this.gameObject);
                }
                agent.SetDestination(exitPos.position);
                return;
            }
            catch 
            {

            }

        }
        if (canWantObject())
        {
            anim.SetInteger("State", 2);
            previewParrent.GetComponentInChildren<Image>().sprite = wantedObject.GetComponent<ObjectProperties>().previewImage;
            previewParrent.SetActive(true);
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
    public void LeaveQueue()
    {
        GameManager.instance.queue[indexCount].clientList[1].GetComponent<AI>().target = null;
        GameManager.instance.queue[indexCount].clientList.RemoveAt(0);
        GameManager.instance.queue[indexCount].currentClientCount--;
    }
    public bool canWantObject()
    {
        float distance = Vector3.Distance(transform.position, GameManager.instance.queue[indexCount].headOfQueue.transform.position);
        if (this.gameObject == GameManager.instance.queue[indexCount].clientList[0].gameObject && distance < 2f)
        {
            return true;
        }
        else
        {
            return false;
        }
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
