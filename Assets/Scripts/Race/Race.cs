using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;

public class Race : MonoBehaviour
{
    [SerializeField] private int _wayPointsCount;

    private List<Track> _tracks;
    
    public bool IsRun { get; private set; }
    private IEnumerable<Runner> Runners => _tracks
        .Where(track => track.IsEmpty == false)
        .Select(track => track.Runner);
    private Track MyTrack { get; set; }
    
    public int RunnersCount => _tracks.Count(runner => runner.IsEmpty == false);
    public bool AreAllRunnersReady => CheckAllRunnersAreReady();
    public Runner MyRunner { get; private set; }

    private void Awake()
    {
        _tracks = GetComponentsInChildren<Track>().ToList();
        if (_tracks.Count == 0)
            throw new Exception("Tracks count should be > 0");

        IsRun = false;
    }
    
    public void Run()
    {
        IsRun = true;
        MyTrack.Run(_wayPointsCount);
    }
    
    public void MarkMyRunnerAsReady()
    {
        MyRunner.MakeReady();
    }
    
    public void RefreshRunners()
    {
        foreach (var track in _tracks)
            track.Free();

        var runners = FindObjectsOfType<Runner>();
        foreach (var runner in runners)
        {
            int id = runner.photonView.Owner.ActorNumber;
            id = (id == -1) ? 0 : id % _tracks.Count;
            
            Add(id, runner);
        }
    }

    private void Add(int trackIndex, Runner runner)
    {
        _tracks[trackIndex].Init(runner);
        
        if (runner.photonView.IsMine)
        {
            MyTrack = _tracks[trackIndex];
            MyRunner = runner;
        }
    }
    
    private bool CheckAllRunnersAreReady()
    {
        var runners = Runners.ToArray();
        return runners.Length != 0 && runners.All(runner => runner.IsReady);
    }
}
