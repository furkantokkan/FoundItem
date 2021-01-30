using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("QueueSettings")]
    public QueueSettings[] queue;
    public static GameManager instance;
    [Header("Object Settings")]
    public GameObject[] beltToSpawn;
    public GameObject[] objectsToSpawn;
    public int maxObjectCount = 10;
    public float objectSpawnRate = 2f;
    internal List<GameObject> SpawnedObjectsList = new List<GameObject>();
    internal int currentObjectCount;
    internal int queueIndex = 0;
    private int objectIndex = -1;
    private bool canSpawnClient = true;
    private bool canSpawnObject = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Update()
    {
        if (canSpawnClient && queue[queueIndex].currentClientCount < queue[queueIndex].maxClientCount)
        {
            StartCoroutine(SpawnNewClient(queueIndex));
            canSpawnClient = false;
            queueIndex++;
            if (queueIndex == queue.Length)
            {
                queueIndex = 0;
            }
        }
        if (canSpawnObject && currentObjectCount < maxObjectCount)
        {
            StartCoroutine(SpawnNewObject());
            canSpawnObject = false;
        }
    }
    public IEnumerator SpawnNewClient(int givenIndex)
    {
        bool finished = false;
        int startCount = queue[givenIndex].currentClientCount;
        while (!finished)
        {
            if (queue[givenIndex].currentClientCount == startCount + 1)
            {
                finished = true;
                break;
            }
            yield return new WaitForSeconds(queue[givenIndex].clientSpawnRate);
            queue[givenIndex].currentClientCount++;
            GameObject client = Instantiate(queue[givenIndex].client[0].gameObject, queue[givenIndex].clientSpawnTransform.position, Quaternion.identity);
            client.GetComponent<AI>().indexCount = givenIndex;
            yield return null;
        }
        canSpawnClient = true;
    }
    public IEnumerator SpawnNewObject()
    {
        bool finished = false;

        while (!finished)
        {
            if (currentObjectCount == maxObjectCount)
            {
                finished = true;
                break;
            }
            yield return new WaitForSeconds(objectSpawnRate);
            currentObjectCount++;
            Transform targetParent = GetRandomBelt().transform;
            GameObject objectClone = Instantiate(GetObject(), targetParent.transform.GetChild(0).transform.position, Quaternion.identity);
            objectClone.GetComponent<FollowCurve>().curveToFollow = targetParent.GetComponentInChildren<BezierCurve>();
            yield return null;
        }
        canSpawnObject = true;
    }

    public GameObject GetObject()
    {
        objectIndex++;
        if (objectIndex == objectsToSpawn.Length)
        {
            objectIndex = 0;
        }
        return objectsToSpawn[objectIndex];

    }
    public GameObject GetRandomObject()
    {
        int index = Random.Range(0, objectsToSpawn.Length);
        return objectsToSpawn[index];
    }
    public GameObject GetRandomBelt()
    {
        int index = Random.Range(0, beltToSpawn.Length);
        return beltToSpawn[index];
    }
}
[System.Serializable]
public class QueueSettings
{
    [Header("QueueSettings")]
    public Transform headOfQueue;
    public Transform clientSpawnTransform;
    internal int currentClientCount;
    internal List<GameObject> clientList = new List<GameObject>();
    [Header("Client Settings")]
    public GameObject[] client;
    public int maxClientCount = 10;
    public float clientSpawnRate = 2f;
}