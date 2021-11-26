using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private PlayerInput playerInput;
    [Header("Movement")]
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;
    [SerializeField] Vector2 rawInput;
    [SerializeField] Vector3 movement_input;
    private Player player;
    private Rigidbody rb;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        playerInput = player.getPlayerInput;
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isMoving)
        {
            Move(rawInput);
            Rotate();//rawInput
        }
    }
    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        isMoving = rawInput == Vector2.zero ? false : true;
        movement_input = new Vector3(rawInput.x, 0f, rawInput.y);
    }
    private void Rotate() //Vector2 input
    {
        // if (input.x == -1)//leftrot
        //     player.rotObject.transform.rotation = Quaternion.Euler(player.rotObject.transform.rotation.x, -90, player.rotObject.transform.rotation.z);
        // else if (input.x == 1) //right rot
        //     player.rotObject.transform.rotation = Quaternion.Euler(player.rotObject.transform.rotation.x, 90, player.rotObject.transform.rotation.z);
        // if (input.y == 1)//front rot
        //     player.rotObject.transform.rotation = Quaternion.Euler(player.rotObject.transform.rotation.x, 0, player.rotObject.transform.rotation.z);
        // else if (input.y == -1) //down rot
        //     player.rotObject.transform.rotation = Quaternion.Euler(player.rotObject.transform.rotation.x, -180, player.rotObject.transform.rotation.z);

        player.rotObject.transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    private void Move(Vector2 input)
    {
        Vector3 moveVector = transform.TransformDirection(movement_input) * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

}
