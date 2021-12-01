using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomAnimation : MonoBehaviour
{
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(0,100);
        GetComponent<Animator>().Play("Iddle", 0, time/100);
        //SetRandomAnimation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
