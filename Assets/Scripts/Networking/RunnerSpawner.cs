using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class RunnerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private Runner _runnerPrefab;
    [SerializeField] private Race _race;
    [SerializeField] private bool _isAutoSpawn = true;
    [SerializeField] private byte _maxPlayersCount = 4;
    [SerializeField] private int _playerTTL = -1;

    private void Start()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public virtual void SpawnRunner()
    {
        var runnerObject = PhotonNetwork.Instantiate(_runnerPrefab.name, Vector3.zero, Quaternion.identity, 0);
        Runner runner = runnerObject.GetComponent<Runner>();
        runner.PlayerNumberIsAssigned += OnPlayerNumberAssigned;

    }
    
    public override void OnJoinedRoom()
    {
        if (_isAutoSpawn && PhotonNetwork.LocalPlayer.HasRejoined == false)
        {
            SpawnRunner();
            // _race.RefreshRunners();
        }
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

    private void OnPlayerNumberAssigned(Runner runner)
    {
        _race.RefreshRunners();
        runner.PlayerNumberIsAssigned -= OnPlayerNumberAssigned;
    }
}

