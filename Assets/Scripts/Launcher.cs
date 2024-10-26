using System.Collections.Generic;
using Components;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;
using UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    
    [SerializeField] private TMP_InputField _roomNameInputField;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _roomNameText;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform _playerListContent;
    [SerializeField] private GameObject _playerListItemPrefab;
    [SerializeField] private GameObject _startGameButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Master servera giriş yapıldı.");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby() 
    {
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Lobiye giriş yapıldı.");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
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

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }

    public override void OnJoinedRoom() 
    {
        MenuManager.Instance.OpenMenu("Room");
        _roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        
        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in _playerListContent)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _errorText.text = "Room creation faied. " + message;
        MenuManager.Instance.OpenMenu("Error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");       
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        
        foreach (RoomInfo item in roomList)
        {
            if (item.RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(item);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListItemPrefab, _playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    
    
}
