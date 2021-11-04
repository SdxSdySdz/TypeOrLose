using UnityEngine;

namespace MainMenu.PigScripts
{
    public class EyeRotator : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _wall;
        [SerializeField] private GameObject _leftEye;
        [SerializeField] private GameObject _rightEye;

        private void Start()
        {
            _wall.transform.LookAt(_camera.transform);
        }

        private void Update()
        {
        
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                Vector3 target = -hit.point;

                _leftEye.transform.LookAt(target);
                _rightEye.transform.LookAt(target);
            }
        
        }
    }
}
