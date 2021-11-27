using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] int player_id;
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput getPlayerInput { get { return playerInput; } }
    public GameObject rotObject; 
        public void Init(int id)
    {   
        playerInput = GetComponent<PlayerInput>();
        this.player_id = id;
    }
}

