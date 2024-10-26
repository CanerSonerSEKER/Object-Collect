using Components;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomNameInputField;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _roomNameText;
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Master servera giriş yapıldı.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() 
    {
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Lobiye giriş yapıldı.");
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInputField.text))
        {
            return;
        }
        
        PhotonNetwork.CreateRoom(_roomNameInputField.text);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnJoinedRoom() 
    {
        MenuManager.Instance.OpenMenu("Room");
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room creation faied. " + message;
        MenuManager.Instance.OpenMenu("Error");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");       
    }
    
    
}
