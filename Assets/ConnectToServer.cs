using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();    
    }
    public override void OnConnectToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJointLobby()
    {  
        SceneManager.LoadScene("MainMenu");        
    }

}
