using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerSC : MonoBehaviour
{
    public float time = 120f;
    public TMPro.TextMeshProUGUI timerText;

    public GameObject InGame;
    public GameObject Finish;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        time -= Time.deltaTime;
        timerText.text = time.ToString("N1");
        if(time <= 0)
        {
            SceneManager.LoadScene(0);
            Finish.SetActive(true);
            GameManager.instance.gameFinished = true;
            InGame.SetActive(false);
        }
        else
        {
            GameManager.instance.gameFinished = false;
        }
    }
}
