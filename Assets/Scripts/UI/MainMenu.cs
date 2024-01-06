using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string VersionName;
    public string MapName;
    public byte MaxPlayers;

    public void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }

    void OnconnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public void StartGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;

        PhotonNetwork.JoinOrCreateRoom(MapName, roomOptions, TypedLobby.Default);
    }

    public void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(MapName);
    }
}
