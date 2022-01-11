using System.Security.AccessControl;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField] public Rooms rooms;
    void Awake() { MasterManager.Instance.setRoomListing = this; }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("Updated room listing, roomlist length is: " + roomList.Count);
        foreach (RoomInfo info in roomList)
        {
            print("Updating room: " + info.Name);
            if (!info.RemovedFromList)
                rooms.AddRoom(info);
            else
                rooms.RemoveRoom(info);
        }
    }

    public void JoinRandomRoom()
    {
        try
        {
            string name = rooms.GetRandomRoomName();
            Debug.Log("User: " + PhotonNetwork.LocalPlayer.NickName + " has joined Room: " + name);
            PhotonNetwork.JoinRoom(name);
            MasterManager.RoomCreator.LoadNewLevel("Lobby");
        }
        catch (System.Exception e)
        {
            Debug.Log("Error being: " + e.Message);
        }
    }

}







[System.Serializable]
public class Rooms
{
    public void AddRoom(RoomInfo _roomInfo)
    {
        if (!_rooms.Contains(_rooms.Find(x => x.RoomName == _roomInfo.Name)))
            _rooms.Add(new Room(_roomInfo));
    }
    public void AddRoom(string _name)
    {
        _rooms.Add(new Room(_name, 0));
    }
    public void RemoveRoom(RoomInfo _roomInfo)
    {
        _rooms.Remove(_rooms.Find(r => r.RoomInfo.Name == _roomInfo.Name));
        _rooms.TrimExcess();
    }
    public void RemoveRoom(string _name)
    {
        _rooms.Remove(_rooms.Find(r => r.RoomName == _name));
        _rooms.TrimExcess();
    }
    public string GetRandomRoomName() { return _rooms[Random.Range(0, _rooms.Count)].RoomName; }
    [SerializeField] private List<Room> _rooms;
    public List<Room> getRooms { get { return _rooms; } }
}
[System.Serializable]
public class Room
{
    public Room(string _roomName, int _maxPlayers)
    {
        this._roomName = _roomName;
        this._maxPlayers = _maxPlayers;
    }
    public Room(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
    }

    [SerializeField] private RoomInfo _roomInfo;
    [SerializeField] private string _roomName;
    [SerializeField] private int _maxPlayers;
    public string RoomName { get { return _roomName; } set { _roomName = value; } }
    public RoomInfo RoomInfo { get { return _roomInfo; } set { _roomInfo = value; _roomName = _roomInfo.Name; _maxPlayers = _roomInfo.MaxPlayers; } }
    public int _MaxPlayers { get { return _maxPlayers; } set { _maxPlayers = value; } }
}


