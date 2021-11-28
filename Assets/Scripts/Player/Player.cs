using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] public int points;
    [SerializeField] public float points_factor = 1f;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] public PlayerMovement movement;
    public PlayerInput getPlayerInput { get { return playerInput; } }
    public GameObject rotObject;

    void Start()
    {
        if (GameManager.Instance.player1 == null){
            GameManager.Instance.player1 = this; gameObject.name = "Player1";}
        else{
            GameManager.Instance.player2 = this; gameObject.name = "Player2";
            GameManager.Instance.StartGame();}
    }
}

