using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCloudAnimator : MonoBehaviour
{
    void Awake()
    {
        if (MasterManager.RoomCreator != null) MasterManager.RoomCreator.cloudsAnimator = GetComponent<Animator>();
        if (MasterManager.CreateAndJoin != null) MasterManager.CreateAndJoin.cloudsAnimator = GetComponent<Animator>();
    }
}
