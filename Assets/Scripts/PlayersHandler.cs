using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersHandler : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private InputActionAsset player1Actions,player2Actions;
 void Start()
 {
     AddPlayers();
 }
    public void AddPlayers()
    {
        GameObject p1 = GameObject.Instantiate(player,Vector3.zero, Quaternion.identity);
        p1.GetComponent<Player>().Init(1,player1Actions);
        GameObject p2 = GameObject.Instantiate(player,Vector3.zero, Quaternion.identity);
        p1.GetComponent<Player>().Init(2,player2Actions);
    }
}
