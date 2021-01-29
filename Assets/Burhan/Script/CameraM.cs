using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    [Range(0.01f,1.0f)]
    public float smooth = 0.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = player.transform.position + offset;

        transform.position = Vector3.Slerp(transform.position, pos, smooth);
    }
}
