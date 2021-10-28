using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform _from;
    [SerializeField] private Transform _to;

    public Transform StartPoint => _from;
    public Runner Runner { get; private set; }

    public bool IsEmpty => Runner == null;

    public void Init(Runner runner)
    {
        Runner = runner;
        SetRunnerAtStart();
    }
    
    public void Run(int wayPointsCount)
    {
        Runner.Run(GenerateWayPoints(wayPointsCount));
    }

    private void SetRunnerAtStart()
    {
        Runner.transform.position = _from.position;
        Runner.transform.rotation = _from.rotation;
        
        Runner.transform.LookAt(_to);
    }

    private Vector3[] GenerateWayPoints(int wayPointsCount)
    {
        var wayPoints = new Vector3[wayPointsCount];
        
        float timeStep = 1f / (wayPointsCount - 1);
        Vector3 from = Vector3.Lerp(_from.position, _to.position, timeStep);
        for (int i = 0; i < wayPointsCount; i++)
        {
            wayPoints[i] = Vector3.Lerp(from, _to.position, i * timeStep);
        }

        return wayPoints;
    }

    public void Free()
    {
        Runner = null;
    }
}
