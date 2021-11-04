using Constants;
using Photon.Pun;
using UnityEngine;

public class ConnectionProvider : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte _version = 1;
    
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = _version + "." + SceneManagerHelper.ActiveSceneBuildIndex;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. This client is now connected to Master Server in region [" +
                  PhotonNetwork.CloudRegion +
                  "] and can join a room.");
        
        PhotonNetwork.LoadLevel(SceneNames.MainMenu);
    }
}
