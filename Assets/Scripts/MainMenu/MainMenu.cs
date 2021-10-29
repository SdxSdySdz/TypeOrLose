using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is now connected to Relay in region [" + PhotonNetwork.CloudRegion +
                  "].");
    }

    public void OnPlayButtonClicked()
    {
        PhotonNetwork.LoadLevel("RaceScene");
    }
    
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
