using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : ScriptableObjectSingleton<MasterManager>
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private RoomListing _roomListing;
    [SerializeField] private CreateRoom _roomCreator;
    [SerializeField] private CreateAndJoin _createAndJoin;
    public static CreateRoom RoomCreator { get { return Instance._roomCreator; } }
    public CreateRoom setRoomCreator { set { _roomCreator = value; } }
    public static RoomListing RoomListing { get { return Instance._roomListing; } }
    public RoomListing setRoomListing { set { _roomListing = value; } }

    public static CreateAndJoin CreateAndJoin { get { return Instance._createAndJoin; } }
    public CreateAndJoin setCreateAndJoin { set { _createAndJoin = value; } }
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }
}
