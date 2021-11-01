using UnityEngine;
using UnityEngine.Events;

public class InputTrigger : MonoBehaviour
{
    private char _target;
    
    public event UnityAction Triggered;

    public void FollowLetter(char target)
    {
        _target = target;
    }

    private void Update()
    {
        var input = Input.inputString;
        if (string.IsNullOrEmpty(input)) return;
        
        if (input[0] == _target)
        {
            Debug.LogError("Good!");
            Triggered?.Invoke();
        }
        else
        {
            Debug.LogError("Bad!");
        }
    }
}
