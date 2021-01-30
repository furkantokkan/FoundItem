using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Queue Settings")]
    public Transform headOfQueue;
    public List<GameObject> clientList = new List<GameObject>();
    [Header("Client Settings")]
    [SerializeField] GameObject client;
    [SerializeField] Transform clientSpawnTransform;
    public int maxClientCount = 10;
    public float clientSpawnRate = 2f;
    [Header("Object Settings")]
    public GameObject[] beltToSpawn;
    public GameObject[] objectsToSpawn;
    public int maxObjectCount = 10;
    public float objectSpawnRate = 2f;

    private List<GameObject> SpawnedObjectsList = new List<GameObject>();
    internal int currentObjectCount;
    internal int currentClientCount;
    private bool canSpawnClient = true;
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
    private void Start()
    {
        StartCoroutine(SpawnNewObject());
    }
    void Update()
    {
        if (canSpawnClient && currentClientCount < maxClientCount)
        {
            StartCoroutine(SpawnNewClient());
            canSpawnClient = false;
        }
    }

    public IEnumerator SpawnNewClient()
    {
        bool finished = false;

        while (!finished)
        {
            if (currentClientCount == maxClientCount)
            {
                finished = true;
                break;
            }
            yield return new WaitForSeconds(clientSpawnRate);
            currentClientCount++;
            Instantiate(client, clientSpawnTransform.position, Quaternion.identity);
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
            GameObject objectClone = Instantiate(GetRandomObject(), targetParent.transform.GetChild(0).transform.position, Quaternion.identity);
            objectClone.GetComponent<FollowCurve>().curveToFollow = targetParent.GetComponentInChildren<BezierCurve>();
        }
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
