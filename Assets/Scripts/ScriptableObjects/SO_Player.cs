using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Object/SO Player", fileName = "SO Player")]
public class SO_Player : ScriptableObject
{
    public PlayerType playerType;
    [ResizableTextArea] public string description;
    public Material mat;
    public int WaterLevel;


}