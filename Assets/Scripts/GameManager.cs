using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Queue Settings")]
    public List<GameObject> currentQueue = new List<GameObject>();
    [Header("Client Settings")]
    public GameObject client;
    public Transform clientSpawnTransform;
    public int maxClientCount = 10;

    private int currentClientCount;
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
            yield return new WaitForSeconds(1.5f);
            currentClientCount++;
            Instantiate(client, clientSpawnTransform.position, Quaternion.identity);
            yield return null;
        }
        canSpawnClient = true;
    }
}
