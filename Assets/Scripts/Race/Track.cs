using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform _from;
    [SerializeField] private Transform _to;

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

    private IEnumerable<Vector3> GenerateWayPoints(int wayPointsCount)
    {
        var wayPoints = new Vector3[wayPointsCount];
        
        var timeStep = 1f / (wayPointsCount - 1);
        var from = Vector3.Lerp(_from.position, _to.position, timeStep);
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
