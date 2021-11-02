using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToMainMenuButton : MonoBehaviour
{
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        PhotonNetwork.LoadLevel("MainMenuScene");
        PhotonNetwork.LeaveRoom();
    }
}