using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Queue Settings")]
    public Transform headOfQueue;
    public List<GameObject> currentQueueList = new List<GameObject>();
    [Header("Client Settings")]
    [SerializeField] GameObject client;
    [SerializeField] Transform clientSpawnTransform;
    public int maxClientCount = 10;
    public float spawnRate = 2f;

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
            yield return new WaitForSeconds(spawnRate);
            currentClientCount++;
            Instantiate(client, clientSpawnTransform.position, Quaternion.identity);
            yield return null;
        }
        canSpawnClient = true;
    }
}
