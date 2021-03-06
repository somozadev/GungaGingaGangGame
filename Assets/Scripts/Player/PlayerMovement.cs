using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.InputSystem;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{



    #region variables
    public TMPro.TMP_Text debug_text;

    [Header("Acting info")]
    [SerializeField] public Joystick joystick;
    [SerializeField] public Button comedy_button;
    [SerializeField] public Button tragedy_button;

    [Header("Acting info")]
    [SerializeField] public Genre current_genre;
    [Header("Stunned")]
    [Space(10)]
    [SerializeField] public bool isStunned;
    [Header("Movement")]
    [Space(10)]
    [SerializeField] public bool MovementBlocked;
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;
    [SerializeField] Vector3 movement_input;
    [Header("Push")]
    [Space(10)]
    [SerializeField] public GameObject target;
    [SerializeField] bool isPushing;
    [SerializeField] private bool canPush = true;
    [SerializeField] float push_cooldown;
    [SerializeField] private float push_power;
    [SerializeField] private float push_penalty_time;


    [Header("Tragedy")]
    [Space(10)]
    [SerializeField] bool isTragedy;
    [Header("Comedy")]
    [Space(10)]
    [SerializeField] bool isComedy;
    [Header("Refs")]
    [Space(10)]
    [SerializeField] public Animator player_animator;

    [SerializeField] Vector2 movement_rawInput;
    private bool push_rawInput;
    private bool tragedy_rawInput;
    private bool comedy_rawInput;
    private Player player;
    private Rigidbody rb;

    #endregion
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        rb = GetComponent<Rigidbody>();
    }
    public void GetStunned(float st)
    {
        isStunned = true;
        MovementBlocked = true;
        player_animator.SetBool("Stunned", true);
        CameraShaker.Instance.ShakeOnce(2f, 2f, 0.1f, 1f);

        StartCoroutine(StunCorr(st));
    }
    private IEnumerator StunCorr(float st)
    {
        float elapsed_time1 = 0;
        while (elapsed_time1 <= st)
        {
            elapsed_time1 += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isStunned = false;
        player_animator.SetBool("Stunned", false);
        elapsed_time = 0f;
        isComedy = false; isTragedy = false;

        isdelay_counter_trag = isdelay_counter_com = false;
        this.current_genre = Genre.NULL;
        player_animator.SetBool("Tragedy", false);
        player_animator.SetBool("Comedy", false);



        MovementBlocked = false;
    }

    void FixedUpdate()
    {
        //movement_rawInput = new Vector2(joystick.Horizontal(), joystick.Vertical());
        //isMoving = joystick.normalizedPoint == Vector2.zero ? false : true;
        //movement_input = new Vector3(movement_rawInput.x, 0f, movement_rawInput.y);

        movement_rawInput = new Vector2(joystick.Horizontal(), joystick.Vertical());
        isMoving = movement_rawInput == Vector2.zero ? false : true;
        movement_input = new Vector3(movement_rawInput.x, 0f, movement_rawInput.y);

        if (!MovementBlocked)
        {
            if (isMoving)
            {
                Move(movement_rawInput);
                Rotate();
                player_animator.SetBool("Moving", true);
            }
            else
                player_animator.SetBool("Moving", false);
            if (isPushing && canPush)
            {
                Push();
            }

        }

    }

    #region inputs
    //private void OnMove(InputValue value)
    //{
        // movement_rawInput = value.Get<Vector2>();
        // isMoving = movement_rawInput == Vector2.zero ? false : true;
        // movement_input = new Vector3(movement_rawInput.x, 0f, movement_rawInput.y);
    //}
    //private void OnPush(InputValue value)
    //{
    //    push_rawInput = System.Convert.ToBoolean(value.Get<float>());
    //    isPushing = push_rawInput;
    //}
    public void OnTragedy()//InputValue value)
    {

        if (!MovementBlocked)
        {
            if (!isComedy)
            {
                Debug.Log("Tragedy callback");
                // tragedy_rawInput = System.Convert.ToBoolean(value.Get<float>());
                isTragedy = true;//tragedy_rawInput;
                isComedy = false;
                Tragedy();
                player_animator.SetBool("Tragedy", true);
            }
        }
    }
    public void OnComedy()//InputValue value)
    {
        if (!MovementBlocked)
        {
            if (!isTragedy)
            {
                // comedy_rawInput = System.Convert.ToBoolean(value.Get<float>());
                isComedy = true;//comedy_rawInput;
                isTragedy = false;
                Comedy();
                player_animator.SetBool("Comedy", true);
            }
        }
    }

    #endregion

    #region push
    private void Push()
    {
        if (target != null)
        {
            if (target.TryGetComponent<Rigidbody>(out Rigidbody rb2))
            {
                if (rb2.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
                {
                    movement.StopAllCoroutines();
                    movement.MovementBlocked = true;
                    movement.player.rotObject.transform.rotation = Quaternion.LookRotation(-player.rotObject.transform.forward);
                    movement.player_animator.SetTrigger("Pushed");
                    rb2.AddForce((player.rotObject.transform.forward) * push_power, ForceMode.VelocityChange);
                    movement.GiveMovementBack();
                }
                else
                    rb2.AddForce((player.rotObject.transform.forward) * push_power, ForceMode.VelocityChange);
            }
            else if (target.transform.parent.TryGetComponent<Rigidbody>(out Rigidbody rb2_2))
            {
                if (rb2_2.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
                {
                    movement.StopAllCoroutines();
                    movement.MovementBlocked = true;
                    movement.player.rotObject.transform.rotation = Quaternion.LookRotation(-player.rotObject.transform.forward);
                    movement.player_animator.SetTrigger("Pushed");
                    rb2_2.AddForce((player.rotObject.transform.forward) * push_power, ForceMode.VelocityChange);
                    movement.GiveMovementBack();
                }
            }

        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        canPush = false;
        player_animator.SetTrigger("Push");


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
    #endregion
    private void Rotate()
    {
        player.rotObject.transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    private void Move(Vector2 input)
    {
        if (input.magnitude > .1f)
        {
            Vector3 moveVector = transform.TransformDirection(movement_input) * speed;
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
        }
    }

    private void Tragedy()
    {
        if (!isdelay_counter_trag && !isdelay_restarted_trag)
        {
            elapsed_time = 0f;
            isdelay_counter_com = false;
            isdelay_restarted_com = false;
            StopCoroutine(ComedyCorr());
            StartCoroutine(TragedyCorr());
        }
        else
        {
            isdelay_restarted_trag = true;
        }

    }
    private void Comedy()
    {
        if (!isdelay_counter_com && !isdelay_restarted_com)
        {
            elapsed_time = 0f;
            isdelay_counter_com = false;
            isdelay_restarted_com = false;
            StopCoroutine(TragedyCorr());
            StartCoroutine(ComedyCorr());
        }
        else
        {
            isdelay_restarted_com = true;
        }

    }
    [SerializeField] private float delay_counter;
    [SerializeField] private float elapsed_time;
    [SerializeField] private bool isdelay_counter_com = false;
    [SerializeField] private bool isdelay_restarted_com = false;
    [SerializeField] private bool isdelay_counter_trag = false;
    [SerializeField] private bool isdelay_restarted_trag = false;
    private IEnumerator TragedyCorr()
    {
        isdelay_counter_trag = true;

        this.current_genre = Genre.TRAGEDY;
        int added = GameManager.Instance.GetPointsToAddPlayer(Genre.TRAGEDY);

        player.points += added;
        if (added > 0)
            Instantiate(pointsPrefab, transform.position + Vector3.up * 3f, Quaternion.identity, transform).GetComponent<PointsIndicator>().SetPoints(added, Genre.TRAGEDY);
        while (elapsed_time <= delay_counter)
        {
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (!isdelay_restarted_trag)
        {
            isdelay_counter_trag = false;
            isTragedy = false;
            this.current_genre = Genre.NULL;
            player_animator.SetBool("Tragedy", false);
        }
        else
        {
            elapsed_time = 0f;
            isdelay_counter_trag = false;
            isdelay_restarted_com = false;
            isdelay_restarted_trag = false;
            StartCoroutine(TragedyCorr());
        }

    }
    public GameObject pointsPrefab;
    private IEnumerator ComedyCorr()
    {
        isdelay_counter_com = true;

        this.current_genre = Genre.COMEDY;
        int added = GameManager.Instance.GetPointsToAddPlayer(Genre.COMEDY);
        player.points += added;
        if (added > 0)
            Instantiate(pointsPrefab, transform.position + Vector3.up * 3f, Quaternion.identity, transform).GetComponent<PointsIndicator>().SetPoints(added, Genre.COMEDY);
        while (elapsed_time <= delay_counter)
        {
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (!isdelay_restarted_com)
        {
            isdelay_counter_com = false;
            isComedy = false;
            this.current_genre = Genre.NULL;
            player_animator.SetBool("Comedy", false);
        }
        else
        {
            elapsed_time = 0f;
            isdelay_counter_com = false;
            isdelay_restarted_com = false;
            isdelay_restarted_trag = false;
            StartCoroutine(ComedyCorr());
        }
    }





}
