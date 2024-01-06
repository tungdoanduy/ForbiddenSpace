using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class GearInfo
{
    public int quantity;
    public string description;
    public GameObject gearTemplate;
}
public class GearDict : SerializableDictionaryBase<GearType,GearInfo> { }

public class BookInfo
{
    public Sprite icon;
    public Material coverMat;
}

public class BookDict : SerializableDictionaryBase<BookType, BookInfo> { }

public class GameManager : MonoBehaviour
{
    public GearDict GearDict;
    public BookDict BookDict;
    public GameObject BookTemplate;

    void SetUp()
    {

    }
}
