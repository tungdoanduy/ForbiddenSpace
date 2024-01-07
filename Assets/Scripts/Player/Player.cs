using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    [SerializeField] SO_Player role;
    [SerializeField] List<MeshRenderer> parts = new List<MeshRenderer>();
    Vector2Int currentTilePos;
    public Vector2Int CurrentTilePos
    {
        get => currentTilePos;
        set => currentTilePos = value;
    }
    public void SetUp()
    {
        foreach (MeshRenderer part in parts)
        {
            part.material = role.mat;
        }
    }
}
