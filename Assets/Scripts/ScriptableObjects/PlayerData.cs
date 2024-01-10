using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/SO Player", fileName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public SO_Player PlayerType;
    public int PlayerID;
    public string PlayerName;

}
