using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{


    [SerializeField] private List<Player> _players;
    [SerializeField] Player player_prefab;


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("NEW PLAYER ENTERED THE ROOM! welcome, " + newPlayer.NickName);
        Player _newPlayer = Instantiate(player_prefab, gameObject.transform);
        _players.Add(_newPlayer);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        _players.Remove(_players.Find(x => x._player == otherPlayer));
        _players.TrimExcess();
    }
}
