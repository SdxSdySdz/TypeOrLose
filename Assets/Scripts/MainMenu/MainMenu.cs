using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _nickName;

    private void Start()
    {
        _nickName.text = PlayerPrefs.GetString(PlayerPrefsConstants.NickName);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). This client is now connected to Relay in region [" + PhotonNetwork.CloudRegion +
                  "].");
    }

    public void OnPlayButtonClicked()
    {
        SaveNickName();
        PhotonNetwork.LoadLevel("RaceScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void SaveNickName()
    {
        string text = _nickName.text;
        PhotonNetwork.NickName = text;
        PlayerPrefs.SetString(PlayerPrefsConstants.NickName, text);
    }
}
