using UnityEngine;

public class CapsLockHint : MonoBehaviour
{
    [SerializeField] private GameObject _hint;
    
    
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private bool IsCapsLockOn => (GetKeyState(0x14) & 1) > 0;
    
    [System.Runtime.InteropServices.DllImport("USER32.dll")] 
    public static extern short GetKeyState(int nVirtKey);
#else
    #error IsCapsLockOn not defined for this platform
#endif

    private void Update()
    {
        _hint.SetActive(IsCapsLockOn);
    }
}
