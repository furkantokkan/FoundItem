using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    public List<GameObject> chars;
    
    void Awake()
    {
        int rand = Random.Range(0, chars.Count);

        chars[rand].SetActive(true);

        GetComponent<AI>().anim = chars[rand].GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
