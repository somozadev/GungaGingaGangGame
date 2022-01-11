using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    [SerializeField] public Rooms rooms;
    [SerializeField] public Animator cloudsAnimator;


    void Awake() { MasterManager.Instance.setCreateAndJoin = this; }


    public void CreateRoom()
    {
        string name = GetNewRoomName();
        rooms.AddRoom(name);
        PhotonNetwork.CreateRoom(name);
    }
    public void JoinRoom()
    {
        string name = rooms.GetRandomRoomName();
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        // Debug.Log("JOIN THE GAME NOW BOIZ ");
        PhotonNetwork.LoadLevel("Lobby");
        // JoinNewPhotonLevel("Lobby");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (!info.RemovedFromList)
                rooms.AddRoom(info);
            else
                rooms.RemoveRoom(info);
        }
    }

    public void LoadNewPhotonLevel(string level) { StartCoroutine(CloudsAnim(level)); }
    public void JoinNewPhotonLevel(string level) { StartCoroutine(CloudsAnimJoin(level)); }
    IEnumerator CloudsAnim(string levelName)
    {
        cloudsAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel(levelName);
    }
    IEnumerator CloudsAnimJoin(string levelName)
    {
        cloudsAnimator.SetTrigger("LoadLevel");
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel(levelName);
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
}

