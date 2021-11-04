using UnityEngine;

namespace MainMenu.PigScripts
{
    public class MouseBasedScaler : MonoBehaviour
    {
        private Vector3 _startScale;
        private Vector3 _endScale;
        private float _upperThreshold;
        private float _lowerThreshold;
    
        private void Start()
        {
            _endScale = transform.localScale;
            _startScale = new Vector3(_endScale.x / 2, _endScale.y / 2, 1);
            _upperThreshold = Screen.width / 2f;
            _lowerThreshold = Screen.width / 4f;
        }

        private void Update()
        {
            float x = Input.mousePosition.x;

            if (x <= _lowerThreshold)
                transform.localScale = _startScale;
            else if (x <= _upperThreshold)
            {
                float timeStep = (Input.mousePosition.x - _lowerThreshold) / (_upperThreshold - _lowerThreshold);
                transform.localScale = Vector3.Lerp(_startScale, _endScale, timeStep);
            }
            else
                transform.localScale = _endScale;
        }
    }
}
