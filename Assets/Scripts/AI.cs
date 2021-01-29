using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public float agentOffset = 1f;
    NavMeshAgent agent;
    Transform target;
    private bool reachedFirstPos = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        GameManager.instance.currentQueueList.Add(this.gameObject);
        target = RequestNewTarget();
    }

    void Update()
    {
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
    }
    Transform RequestNewTarget()
    {
        if (this.gameObject == GameManager.instance.currentQueueList[0].gameObject)
        {
            agentOffset = 0.1f;
            return GameManager.instance.headOfQueue.transform;
        }
        else
        {
            Transform newTarget = GameManager.instance.currentQueueList[GameManager.instance.currentClientCount - 2].transform;
            return newTarget;
        }
    }
}
