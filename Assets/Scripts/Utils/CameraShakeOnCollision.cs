using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShakeOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CameraShakeInstance");
        CameraShaker.Instance.ShakeOnce(1f, 1f, 0.1f, 1f);
    }
}
