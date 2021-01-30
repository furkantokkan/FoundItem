using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSC : MonoBehaviour
{
    public float time = 120f;
    public UnityEngine.UI.Text timerText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = time.ToString("N1");
    }
}
