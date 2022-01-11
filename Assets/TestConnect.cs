using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class TestConnect : MonoBehaviourPunCallbacks
{
    [SerializeField] Player player;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        print("Connecting to server.");
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName; 
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        print("Connected to server.");
        print(PhotonNetwork.LocalPlayer.NickName);
        SceneManager.LoadScene("MainMenu");

        PhotonNetwork.JoinLobby();

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected due to " + cause.ToString());
    }
}
