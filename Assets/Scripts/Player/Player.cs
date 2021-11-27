using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int player_id;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput getPlayerInput { get { return playerInput; } }


    public GameObject rotObject; 
    public void Init(int id, InputActionAsset actions)
    {   
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions=actions;
        this.player_id = id;
    }
}

[System.Serializable]
public class PlayerStats
{

}
