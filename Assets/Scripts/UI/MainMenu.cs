using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string versionName;
    [SerializeField] string queueScene;
    byte maxPlayers = 2;
    [SerializeField] TMP_Text maxPlayersText;


    [SerializeField] List<string> existedRoomNames = new List<string>();
    public List<string> ExistedRoomNames => existedRoomNames;
    [SerializeField,ReadOnly] string currentChosenRoomName;
    [SerializeField] InputField nameInput;
    [SerializeField] GameObject createGamePanel;
    [SerializeField] GameObject joinGamePanel;
    [SerializeField] RectTransform menuPage;


    public void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }

    void OnConnectedToServer()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public void ToCreateGamePanel()
    {

    }

    public void ToJoinGamePanel()
    {

    }

    public void CreateGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        string roomName = Random.Range(0, 1000000).ToString().PadLeft(6, '0');

        PhotonNetwork.CreateRoom(queueScene, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel(queueScene);
    }

    public void JoinGame(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
