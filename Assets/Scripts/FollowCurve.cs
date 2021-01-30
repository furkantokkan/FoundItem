using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurve : MonoBehaviour
{
    public BezierCurve curveToFollow;
    public float timeBetwenPoints = 1f;
    private List<Transform> pointList = new List<Transform>();
    private int index = 0;
    private Rigidbody rb;
    public bool test;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        for (int i = 0; i < curveToFollow.transform.childCount; i++)
        {
            pointList.Add(curveToFollow.transform.GetChild(i));
        }
        GameManager.instance.SpawnedObjectsList.Add(this.gameObject);
        StartCoroutine(MoveToPoint());
    }
    private void Update()
    {
        if (test)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            StopAllCoroutines();
            enabled = false;
        }
    }
    IEnumerator MoveToPoint()
    {
        bool reached = false;
        int currentIndex = GetNextIndex();

        while (!reached)
        {
            if (Vector3.Distance(transform.position, pointList[currentIndex].transform.position) < 0.15f)
            {
                reached = true;
                break;
            }

            transform.position = Vector3.MoveTowards(transform.position, pointList[currentIndex].transform.position, timeBetwenPoints * Time.deltaTime);
            yield return null;
        }
        if (currentIndex != curveToFollow.transform.childCount - 1)
        {
            StartCoroutine(MoveToPoint());
        }
        else
        {
            GameManager.instance.SpawnedObjectsList.Remove(this.gameObject);
            GameManager.instance.currentObjectCount--;
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            StopAllCoroutines();
            enabled = false;
        }
    }
    public int GetNextIndex()
    {
        return index++;
    }

}