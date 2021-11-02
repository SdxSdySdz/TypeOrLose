using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsFollower : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier;
    
    private Stack<Vector3> _wayPoints;
    private Coroutine _moveCoroutine;
    private float _speed;
    
    public void Init(Stack<Vector3> wayPoints)
    {
        _wayPoints = wayPoints;
    }

    public void MakeStep()
    {
        if (_wayPoints.Count == 0)
            return;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        Vector3 target = _wayPoints.Pop();
        _moveCoroutine = StartCoroutine(Move(target));
    }
    
    private IEnumerator Move(Vector3 target)
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        
        Vector3 difference = (target - transform.position);
        float accuracy = 0.001f;
        while (difference.magnitude > accuracy)
        {
            _speed = SolveSpeed(difference.magnitude);
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.fixedDeltaTime);
            
            difference = target - transform.position;
            yield return waitForFixedUpdate;
        }
    }

    private float SolveSpeed(float distanceToWayPoint)
    {
        return distanceToWayPoint * _speedMultiplier;
    }
}
