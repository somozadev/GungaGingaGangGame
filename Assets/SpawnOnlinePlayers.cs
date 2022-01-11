using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpawnOnlinePlayers : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public Vector3 p1, p2;


    void Start()
    {
        if (FindObjectsOfType<Player>().Count() == 0)
            PhotonNetwork.Instantiate(playerPrefab.name, p1, Quaternion.Euler(0, 180, 0));
        else
            PhotonNetwork.Instantiate(playerPrefab.name, p2, Quaternion.Euler(0, 180, 0));
    }
}
