using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
[RequireComponent(typeof(WayPointsFollower))] 
[RequireComponent(typeof(Rigidbody))]
public class Runner : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private TMP_Text _nickName;
    
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

    // TODO Если заспавнить игрока и сразу нажать кнопку Exit, то код продолжит выполнение в главном меню и сламается на FindObjectOfType<Race>().RefreshRunners();
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => photonView.Owner.GetPlayerNumber() >= 0);

        _isReady = false;
        
        _wayPointsFollower = GetComponent<WayPointsFollower>();
        
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;

        string nickname = photonView.Owner.NickName;
        _nickName.text = nickname;

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
