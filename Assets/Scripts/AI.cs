using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class AI : MonoBehaviour
{
    [Header("Move Settings")]
    public float agentOffset = 1f;
    [Header("Object Pick Settings")]
    public GameObject wantedObject;
    public GameObject previewParrent;

    NavMeshAgent agent;
    Transform target;
    private bool objectTaked = false;
    private bool reachedFirstPos = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        GameManager.instance.clientList.Add(this.gameObject);
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
                Vector3 newPos = new Vector3(GameManager.instance.headOfQueue.position.x, transform.position.y, target.transform.position.z);
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
    bool canWantObject()
    {
        if (transform.position.x >= GameManager.instance.headOfQueue.transform.position.x
          && transform.position.z >= GameManager.instance.headOfQueue.transform.position.z &&
          this.gameObject == GameManager.instance.clientList[0].gameObject)
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
        GameManager.instance.clientList.Remove(this.gameObject);
        GameManager.instance.currentClientCount--;
    }
    Transform RequestNewTarget()
    {
        if (this.gameObject == GameManager.instance.clientList[0].gameObject)
        {
            agentOffset = 0.1f;
            return GameManager.instance.headOfQueue.transform;
        }
        else
        {
            Transform newTarget = GameManager.instance.clientList[GameManager.instance.currentClientCount - 2].transform;
            return newTarget;
        }
    }

}
