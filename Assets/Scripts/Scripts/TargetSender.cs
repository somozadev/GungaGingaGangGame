using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSender : MonoBehaviour
{
    public PlayerMovement movement;
    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Interactuable"))
        {
            movement.target = other.gameObject;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Interactuable"))
        {
            movement.target = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        movement.target = null;
    }
}
