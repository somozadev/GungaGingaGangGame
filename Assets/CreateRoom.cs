using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    public Animator cloudsAnimator;

    void Awake() { MasterManager.Instance.setRoomCreator = this; }



    public void CreateNewRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.IsVisible = true;
        string name = GetNewRoomName();
        PhotonNetwork.JoinOrCreateRoom(name, options, TypedLobby.Default);


    }

    private string GetNewRoomName()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string seed = "";
        for (int i = 0; i < 10; i++)
        {
            seed += chars[Random.Range(0, chars.Length)];
        }
        return seed;
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully.");
        LoadNewLevel("Lobby");
        base.OnCreatedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Created room failed.");
        base.OnCreateRoomFailed(returnCode, message);
    }

    public void LoadNewLevel(string level) { StartCoroutine(CloudsAnim(level)); }
    IEnumerator CloudsAnim(string levelName)
    {
        cloudsAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelName);
    }

}
