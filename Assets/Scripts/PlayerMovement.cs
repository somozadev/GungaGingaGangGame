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
            Rotate();
        }
    }
    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        isMoving = rawInput == Vector2.zero ? false : true;
        movement_input = new Vector3(rawInput.x, 0f, rawInput.y);
    }
    private void Rotate()
    {
        player.rotObject.transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    private void Move(Vector2 input)
    {
        Vector3 moveVector = transform.TransformDirection(movement_input) * speed;
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
    }

}
