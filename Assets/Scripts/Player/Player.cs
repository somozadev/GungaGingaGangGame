using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] public int points;
    [SerializeField] public float points_factor = 1f;

    [SerializeField] Material p1mat, p2mat;
    [SerializeField] GameObject meshMat;

    //[SerializeField] private PlayerInput playerInput;
    [SerializeField] public PlayerMovement movement;
    //public PlayerInput getPlayerInput { get { return playerInput; } }
    public GameObject rotObject;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (GameManager.Instance.player1 == null)
            {
                GameManager.Instance.player1 = this;
                meshMat.GetComponent<SkinnedMeshRenderer>().material = p1mat;
                transform.position = Vector3.zero;//GameManager.Instance.player1_pos.position;
                transform.rotation = GameManager.Instance.player1_pos.rotation;
            }

            // else
            // {
            //     GameManager.Instance.player2 = this;
            //     meshMat.GetComponent<SkinnedMeshRenderer>().material = p2mat;
            //     transform.position = GameManager.Instance.player2_pos.position;
            //     transform.rotation = GameManager.Instance.player2_pos.rotation;//new Vector3(-9, 0, -2);
            // }
        }
        else
        {
            if (GameManager.Instance.player1 == null)
            {
                transform.position = Vector3.zero;//GameManager.Instance.player1_pos.position;
                transform.rotation = GameManager.Instance.player1_pos.rotation;
                GameManager.Instance.player1 = this; gameObject.name = "Player1"; meshMat.GetComponent<SkinnedMeshRenderer>().material = p1mat;
            }
            // else
            // {
            //     transform.position = GameManager.Instance.player2_pos.position;
            //     transform.rotation = GameManager.Instance.player2_pos.rotation;
            //     GameManager.Instance.player2 = this; gameObject.name = "Player2";
            //     meshMat.GetComponent<SkinnedMeshRenderer>().material = p2mat;
            //     GameManager.Instance.StartGame();
            // }
        }
    }
}

