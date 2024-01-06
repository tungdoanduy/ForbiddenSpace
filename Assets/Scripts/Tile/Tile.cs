using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] int layer = 1;
    public int Layer 
    { 
        get => layer; 
        set
        {
            if (value == 0)
                blockType = BlockType.EXCAVATE;
            else if (value == 1)
                blockType = BlockType.SAND;
            else
                blockType = BlockType.BLOCKED_SAND;
            layer = value;
        } 
    }
    [SerializeField] TileType tileType;
    public TileType TileType
    {
        get => tileType;
        set => tileType = value;
    }
    [SerializeField] BlockType blockType;
    public BlockType BlockType
    {
        get => blockType;
        set => blockType = value;
    }
    [SerializeField] GearType gearType;
    public GearType GearType
    {
        get => gearType;
        set => gearType = value;
    }
    [SerializeField] BookType bookType;
    public BookType BookType
    {
        get => bookType;
        set => bookType = value;
    }

    public void SetUp(TileType tileType = TileType.NONE,int layer = 1)
    {
        this.tileType = tileType;
        this.Layer = layer;
    }

    public void CreateTornado()
    {
        this.blockType = BlockType.TORNADO;
        //to do more
    }

    public void CreateGear(GearType gearType)
    {
        this.gearType = gearType;
        //to do more
    }

    public void CreateBook(BookType bookType)
    {
        this.bookType = bookType;
        if (tileType == TileType.PIECE_TRACKER_HORIZONTAL || tileType == TileType.PIECE_TRACKER_VERTICAL)
            return;
    }
}
