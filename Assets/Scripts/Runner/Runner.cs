using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
[RequireComponent(typeof(WayPointsFollower))] 
[RequireComponent(typeof(Rigidbody))]
public class Runner : MonoBehaviourPun, IPunObservable
{
    private WayPointsFollower _wayPointsFollower;
    private Rigidbody _rigidbody;
    private bool _isReady;

    public bool IsReady
    {
        get => _isReady;
        set => _isReady = value;
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        stream.Serialize(ref _isReady);
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => photonView.Owner.GetPlayerNumber() >= 0);

        _isReady = false;
        _wayPointsFollower = GetComponent<WayPointsFollower>();
        
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;

        FindObjectOfType<Race>().RefreshRunners();
    }

    public void Run(IEnumerable<Vector3> wayPoints)
    {
        _wayPointsFollower.Init(new Stack<Vector3>(wayPoints.Reverse()));
    }
    
    public void MakeStep()
    {
        _wayPointsFollower.MakeStep();
    }
}
