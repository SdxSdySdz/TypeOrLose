using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonJoiner : MonoBehaviourPunCallbacks
{
    [SerializeField] private bool _autoConnect = true;
    [SerializeField] private byte Version = 1;
    [Tooltip(
        "The max number of players allowed in room. Once full, " +
        "a new room will be created by the next connection attemping to join.")]
    [SerializeField] private byte _maxPlayersCount = 4;
    [SerializeField] private int _playerTTL = -1;

    public void Start()
    {
        if (_autoConnect)
            ConnectNow();
    }

    public void ConnectNow()
    {
        Debug.Log("ConnectAndJoinRandom.ConnectNow() will now call: PhotonNetwork.ConnectUsingSettings().");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. This client is now connected to Master Server in region [" +
                  PhotonNetwork.CloudRegion +
                  "] and can join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is now connected to Relay in region [" + PhotonNetwork.CloudRegion +
                  "]. This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room in region [" + PhotonNetwork.CloudRegion +
                  "]. Game is now running.");
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available in region [" +
                  PhotonNetwork.CloudRegion +
                  "], so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

        RoomOptions roomOptions = new RoomOptions() {MaxPlayers = _maxPlayersCount};
        if (_playerTTL >= 0)
            roomOptions.PlayerTtl = _playerTTL;

        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected(" + cause + ")");
    }
}