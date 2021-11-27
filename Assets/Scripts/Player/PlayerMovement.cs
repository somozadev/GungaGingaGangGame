using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Acting info")]
    [SerializeField] private Genre current_genre;

    [Header("Movement")]
    [Space(10)]
    [SerializeField] public bool MovementBlocked;
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;
    [SerializeField] Vector3 movement_input;
    [Header("Push")]
    [Space(10)]
    [SerializeField] bool isPushing;
    private bool canPush = true;
    [SerializeField] float push_cooldown;
    [SerializeField] private float push_power;
    [SerializeField] private float push_penalty_time;

    [Header("Tragedy")]
    [Space(10)]
    [SerializeField] bool isTragedy;
    [Header("Comedy")]
    [Space(10)]
    [SerializeField] bool isComedy;

    private Vector2 movement_rawInput;
    private bool push_rawInput;
    private bool tragedy_rawInput;
    private bool comedy_rawInput;
    private Player player;
    private Rigidbody rb;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isMoving && !MovementBlocked)
        {
            Move(movement_rawInput);
            Rotate();
        }
        if (isPushing && canPush)
            Push();
    }
    private void OnMove(InputValue value)
    {
        movement_rawInput = value.Get<Vector2>();
        isMoving = movement_rawInput == Vector2.zero ? false : true;
        movement_input = new Vector3(movement_rawInput.x, 0f, movement_rawInput.y);
    }
    private void OnPush(InputValue value)
    {
        push_rawInput = System.Convert.ToBoolean(value.Get<float>());
        isPushing = push_rawInput;
    }

    private void OnTragedy(InputValue value)
    {
        tragedy_rawInput = System.Convert.ToBoolean(value.Get<float>());
        isTragedy = tragedy_rawInput;
    }

    private void OnComedy(InputValue value)
    {
        comedy_rawInput = System.Convert.ToBoolean(value.Get<float>());
        isComedy = comedy_rawInput;
    }



    private void Push()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        canPush = false;

        //pushanim


        RaycastHit hit;
        Vector3 og = new Vector3(player.rotObject.transform.position.x, 2f, player.rotObject.transform.position.z);
        if (Physics.SphereCast(og, 1f, transform.TransformDirection(player.rotObject.transform.forward), out hit, 1f))
        {
            Debug.DrawRay(og, transform.TransformDirection(player.rotObject.transform.forward) * hit.distance, Color.blue);
            if (hit.transform.tag.Equals("Interactuable"))
            {
                Rigidbody rb2 = hit.transform.GetComponent<Rigidbody>();
                if (rb2.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
                {
                    Debug.Log("SE DEBE ESTAR BLOKIANDO O KE LO KE ");
                    movement.StopAllCoroutines();
                    movement.MovementBlocked = true;
                    movement.GiveMovementBack();
                    // movement.StartCoroutine(movement.ReturnMovement());
                }
                rb2.AddForce((rb2.transform.position - rb.transform.position) * push_power, ForceMode.VelocityChange);
            }
        }

        StartCoroutine(PushCd());

    }
    public void GiveMovementBack()
    {
        StopCoroutine(ReturnMovement());
        StartCoroutine(ReturnMovement());
    }
    private IEnumerator ReturnMovement()
    {
        float elapsed_time = 0f;
        while (elapsed_time <= push_penalty_time)
        {
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return (MovementBlocked = false);
    }
    private IEnumerator PushCd()
    {
        float elapsed_time = 0f;
        while (elapsed_time <= push_cooldown)
        {
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canPush = true;
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
