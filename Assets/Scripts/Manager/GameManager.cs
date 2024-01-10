using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class GearInfo
{
    public int quantity;
    public string description;
    public GameObject gearTemplate;
}
[System.Serializable]
public class GearDict : SerializableDictionaryBase<GearType,GearInfo> { }

[System.Serializable]
public class BookInfo
{
    public Sprite icon;
    public Material coverMat;
}
[System.Serializable]
public class BookDict : SerializableDictionaryBase<BookType, BookInfo> { }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public GearDict GearDict;
    public BookDict BookDict;
    [SerializeField] List<Player> players = new List<Player>();
    int playerSeqNum = -1;
    public List<Player> Players => players;

    int bookLeft = 4;

    private void Awake()
    {
        Instance = this;
    }

    [PunRPC]
    void SetUp()
    {
        playerSeqNum = PhotonNetwork.room.PlayerCount - 1;

    }
}
