using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum RING_TYPE { JOIN, CREATE, EXIT }
public class RingLoader : MonoBehaviour
{

    public bool TwoPlayers = false;

    public bool player1In, player2In;
    public float timer;
    public float maxtime;
    public RING_TYPE ring_type;
    public Image fillImage;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        if (TwoPlayers)
        {
            if (player1In && player2In && (timer < maxtime))
                timer += Time.deltaTime;
            else
            {
                if (timer != maxtime)
                {
                    timer = 0f;
                }
            }
        }
        else
        {
            if (player1In && (timer < maxtime))
                timer += Time.deltaTime;
            else
            {
                if (timer != maxtime)
                {
                    timer = 0f;
                }
            }
        }



        if (timer > maxtime)
        {
            timer = maxtime;
            if (ring_type.Equals(RING_TYPE.CREATE))
                MasterManager.CreateAndJoin.CreateRoom();// MasterManager.RoomCreator.CreateNewRoom();
            else if (ring_type.Equals(RING_TYPE.JOIN))
                MasterManager.CreateAndJoin.JoinRoom();//MasterManager.RoomListing.JoinRandomRoom();
            else
                Application.Quit(0);
        }
        fillImage.fillAmount = timer / maxtime;
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Movement")
        {
            if (collider.GetComponentInParent<Player>() != null)
            {
                if (TwoPlayers)
                {
                    if (!player1In)
                        player1In = true;
                    else
                        player2In = true;
                }
                else
                {
                    if (!player1In)
                        player1In = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Movement")
        {
            if (collider.GetComponentInParent<Player>() != null)
            {
                if (TwoPlayers)
                {
                    if (player1In)
                        player1In = false;
                    else
                        player2In = false;
                }
                else
                {
                    if (player1In)
                        player1In = false;
                }
                fillImage.fillAmount = 0;
                timer = 0;
            }
        }
    }
}
