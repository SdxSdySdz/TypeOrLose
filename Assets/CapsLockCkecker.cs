using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class CapsLockCkecker : MonoBehaviour
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    [System.Runtime.InteropServices.DllImport("USER32.dll")] 
    public static extern short GetKeyState(int nVirtKey);
    
    bool IsCapsLockOn => (GetKeyState(0x14) & 1) > 0;
#else
    #error IsCapsLockOn not defined for this platform
#endif

    private void Update()
    {
        /*if (IsCapsLockOn) {
            Debug.LogError("The Caps Lock key is ON.");
        }
        else {
            Debug.LogError("The Caps Lock key is OFF.");
        }*/
    }
}
