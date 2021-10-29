using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBasedScaler : MonoBehaviour
{
    private Vector3 _startScale;
    private Vector3 _endScale;
    private float _threshold;
    
    private void Start()
    {
        _endScale = transform.localScale;
        _startScale = new Vector3(_endScale.x / 2, _endScale.y / 2, 1);
        _threshold = Screen.width / 2f;
    }

    private void Update()
    {
        float x = Input.mousePosition.x;
        if (x < _threshold)
        {
            float timeStep = Input.mousePosition.x / _threshold;
            transform.localScale = Vector3.Lerp(_startScale, _endScale, timeStep);
        }
        
    }
}
