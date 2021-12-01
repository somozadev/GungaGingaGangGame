using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotator : MonoBehaviour
{
    
    public Vector3 rotateVector = Vector3.zero;
    public enum spaceEnum { Local, World };
    public spaceEnum rotateSpace;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotateSpace == spaceEnum.Local)
            transform.Rotate(rotateVector * Time.deltaTime);
        if (rotateSpace == spaceEnum.World)
            transform.Rotate(rotateVector * Time.deltaTime, Space.World);
    }
    
}
