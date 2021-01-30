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
    private bool check;
    private bool droped;
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
        if (collision.gameObject.tag == "Player")
        {
            if (check == false)
            {
                GameManager.instance.currentObjectCount--;
                GameManager.instance.SpawnedObjectsList.Remove(this.gameObject);
                check = true;
                droped = true;
            }
            rb.isKinematic = false;
            rb.useGravity = true;
            StopAllCoroutines();
        }
       else if (collision.collider.tag == "Belt")
        {
            if (droped)
            {
                StartCoroutine(MoveToPoint());
                if (index == pointList.Count - 1)
                {
                    index--;
                }
                rb.isKinematic = false;
                rb.useGravity = true;
                GameManager.instance.currentObjectCount++;
                GameManager.instance.SpawnedObjectsList.Add(this.gameObject);
                check = false;
                droped = false;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (check == false)
            {
                GameManager.instance.currentObjectCount--;
                GameManager.instance.SpawnedObjectsList.Remove(this.gameObject);
                check = true;
                droped = true;
            }
            rb.isKinematic = false;
            rb.useGravity = true;
            StopAllCoroutines();
        }
        else if (collision.collider.tag == "Belt")
        {
            if (droped)
            {
                StartCoroutine(MoveToPoint());
                if (index == pointList.Count - 1)
                {
                    index--;
                }
                rb.isKinematic = false;
                rb.useGravity = true;
                GameManager.instance.currentObjectCount++;
                GameManager.instance.SpawnedObjectsList.Add(this.gameObject);
                check = false;
                droped = false;
            }
        }
    }
    public int GetNextIndex()
    {
        return index++;
    }

}