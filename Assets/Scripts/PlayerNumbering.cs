using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerNumbering : MonoBehaviourPunCallbacks
{
    private static PlayerNumbering _instance;
    private static Player[] _sortedPlayers;
    [SerializeField] private bool _dontDestroyOnLoad = true;
    
    public delegate void PlayerNumberingChanged();
    public static event PlayerNumberingChanged OnPlayerNumberingChanged;
    public static string RoomPlayerIndexedProp => "pNr";

    public void Awake()
    {
        if (_instance != null && _instance != this && _instance.gameObject != null)
        {
            DestroyImmediate(_instance.gameObject);
        }

        _instance = this;
        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        RefreshData();
    }
    
    public override void OnJoinedRoom()
    {
        RefreshData();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Remove(RoomPlayerIndexedProp);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RefreshData();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogError("Player left room");
        RefreshData();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps != null && changedProps.ContainsKey(RoomPlayerIndexedProp))
        {
            RefreshData();
        }
    }
    
    public void RefreshData()
    {
        if (PhotonNetwork.CurrentRoom == null)
            return;

        if (PhotonNetwork.LocalPlayer.GetPlayerNumber() >= 0)
        {
            _sortedPlayers = PhotonNetwork.CurrentRoom.Players.Values.OrderBy((p) => p.GetPlayerNumber()).ToArray();
            OnPlayerNumberingChanged?.Invoke();
            return;
        }

        HashSet<int> usedInts = new HashSet<int>();
        Player[] sorted = PhotonNetwork.PlayerList.OrderBy((p) => p.ActorNumber).ToArray();
        
        foreach (Player player in sorted)
        {
            int number = player.GetPlayerNumber();

            if (player.IsLocal)
            {
                Debug.Log("PhotonNetwork.CurrentRoom.PlayerCount = " + PhotonNetwork.CurrentRoom.PlayerCount);

                for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
                {
                    if (usedInts.Contains(i) == false)
                    {
                        player.SetPlayerNumber(i);
                        break;
                    }
                }

                break;
            }
  
            if (number < 0)
                break;

            usedInts.Add(number);
        }

        _sortedPlayers = PhotonNetwork.CurrentRoom.Players.Values.OrderBy((p) => p.GetPlayerNumber()).ToArray();
        OnPlayerNumberingChanged?.Invoke();
    }
}

public static class PlayerNumberingExtensions
{
    public static int GetPlayerNumber(this Player player)
    {
        if (player == null)
            return -1;

        if (PhotonNetwork.OfflineMode)
            return 0;

        if (PhotonNetwork.IsConnectedAndReady == false)
            return -1;

        if (player.CustomProperties.TryGetValue(PlayerNumbering.RoomPlayerIndexedProp, out var value))
            return (byte) value;

        return -1;
    }
    
    public static void SetPlayerNumber(this Player player, int playerNumber)
    {
        if (player == null || PhotonNetwork.OfflineMode) return;

        if (playerNumber < 0)
        {
            Debug.LogWarning("Setting invalid playerNumber: " + playerNumber + " for: " + player.ToStringFull());
        }

        if (PhotonNetwork.IsConnectedAndReady == false)
        {
            Debug.LogWarning("SetPlayerNumber was called in state: " + PhotonNetwork.NetworkClientState +
                             ". Not IsConnectedAndReady.");
            return;
        }

        int current = player.GetPlayerNumber();
        if (current != playerNumber)
        {
            Debug.Log("PlayerNumbering: Set number " + playerNumber);
            player.SetCustomProperties(new Hashtable() {{PlayerNumbering.RoomPlayerIndexedProp, (byte) playerNumber}});
        }
    }
}
