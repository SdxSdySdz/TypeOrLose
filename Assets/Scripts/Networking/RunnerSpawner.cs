using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.Serialization;

using Photon.Realtime;
using Photon.Pun;

public class RunnerSpawner : MonoBehaviour, IMatchmakingCallbacks
{
    [SerializeField] private Runner _runnerPrefab;
    [SerializeField] private bool _isAutoSpawn = true;

    private Stack<GameObject> _spawnedObjects;

    public virtual void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public virtual void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        _spawnedObjects = new Stack<GameObject>();
    }
    
    public virtual void OnJoinedRoom()
    {
        // Only AutoSpawn if we are a new ActorId. Rejoining should reproduce the objects by server instantiation.
        if (_isAutoSpawn && PhotonNetwork.LocalPlayer.HasRejoined == false)
            SpawnRunner();
    }
    
    public virtual void SpawnRunner()
    {
        /*
        Vector3 spawnPos;
        Quaternion spawnRot;
        GetSpawnPoint(out spawnPos, out spawnRot);
        */

        var newRunner = PhotonNetwork.Instantiate(_runnerPrefab.name, Vector3.zero, Quaternion.identity, 0);

        /*int id = PhotonNetwork.LocalPlayer.ActorNumber;
        id = (id == -1) ? 0 : id % _spawnPoints.Count;
        _race.Add(id, newRunner.GetComponent<Runner>());*/

        _spawnedObjects.Push(newRunner);
    }

    public virtual void DespawnObjects(bool localOnly)
    {
        while (_spawnedObjects.Count > 0)
        {
            var go = _spawnedObjects.Pop();
            if (go)
            {
                if (localOnly)
                    Destroy(go);
                else
                    PhotonNetwork.Destroy(go);
            }
        }
    }

    public virtual void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public virtual void OnCreatedRoom()
    {
    }

    public virtual void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public virtual void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public virtual void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public virtual void OnLeftRoom()
    {
    }

    /*
    public virtual void GetSpawnPoint(out Vector3 spawnPos, out Quaternion spawnRot)
    {
        Transform point = GetSpawnPoint();
        
        spawnPos = point.position;
        spawnRot = point.rotation;
    }

    protected virtual Transform GetSpawnPoint()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            throw new Exception("There are no any spawn point");
        }
        else
        {
            int id = PhotonNetwork.LocalPlayer.ActorNumber;
            return _spawnPoints[(id == -1) ? 0 : id % _spawnPoints.Count];
        }
    }*/
}

