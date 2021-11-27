using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Acting info")]
    [SerializeField] private Genre current_genre;
    [Header("Taunted effect")]
    [Space(10)]
    [SerializeField] public bool isTaunted;
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
    [SerializeField] private float push_spherecast_radius;
    [SerializeField] private float current_push_range_detection;
    [SerializeField] private float max_push_range_detection;


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
        if (!MovementBlocked)
        {
            if (isMoving) { Move(movement_rawInput); Rotate(); }
            if (isPushing && canPush)
                Push();
            if (isTragedy && !isComedy)
                Tragedy();
            if (isComedy && !isTragedy)
                Comedy();
        }
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
        if (Physics.SphereCast(og, push_spherecast_radius, transform.TransformDirection(player.rotObject.transform.forward), out hit, current_push_range_detection))
        {
            current_push_range_detection = hit.distance;
            if (hit.transform.tag.Equals("Interactuable"))
            {
                Rigidbody rb2 = hit.transform.GetComponent<Rigidbody>();
                rb2.AddForce((rb2.transform.position - rb.transform.position) * push_power, ForceMode.VelocityChange);
                if (rb2.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
                {
                    movement.StopAllCoroutines();
                    movement.MovementBlocked = true;
                    movement.player.rotObject.transform.rotation = Quaternion.LookRotation(player.rotObject.transform.position);
                    movement.GiveMovementBack();
                }
            }
        }
        else
            current_push_range_detection = max_push_range_detection;


        StartCoroutine(PushCd());

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 og = new Vector3(player.rotObject.transform.position.x, 2f, player.rotObject.transform.position.z);

        Debug.DrawLine(og, og + transform.TransformDirection(player.rotObject.transform.forward) * current_push_range_detection);
        Gizmos.DrawWireSphere(og + transform.TransformDirection(player.rotObject.transform.forward) * current_push_range_detection, push_spherecast_radius);
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

    private void Tragedy() { this.current_genre = Genre.TRAGEDY; }
    private void Comedy() { this.current_genre = Genre.COMEDY; }

}
