using System.Collections;
using Photon.Pun;
using UnityEngine;

public class CountDownState : GameState
{
    private Coroutine _countdownCoroutine;

    protected override void OnEnter()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }

        PhotonNetwork.CurrentRoom.IsOpen = false;
        _countdownCoroutine = StartCoroutine(MakeCountdown());
    }
    
    private IEnumerator MakeCountdown()
    {
        var waitForSecondsRealtime = new WaitForSecondsRealtime(1);
        for (int i = 3; i >= 1; i--)
        {
            Debug.LogError($"===Countdown {i}===");
            yield return waitForSecondsRealtime;
        }

        Race.Run();
    }
}
