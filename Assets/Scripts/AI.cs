using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.currentQueue.Add(this.gameObject);
    }

    void Update()
    {
        
    }
}
