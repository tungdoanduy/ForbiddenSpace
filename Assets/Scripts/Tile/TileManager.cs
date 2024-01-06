using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance {  get; private set; }

    [SerializeField] float tileDistance;
    [SerializeField] int row, col;
    [SerializeField] Transform tileContainer;
    [SerializeField] List<List<Tile>> tiles = new List<List<Tile>>();
    [SerializeField] Vector2Int tornadoPos,launchpadPos;
    [SerializeField] GameManager gameManager;//just for test, must be deleted later
    [SerializeField]

    private void Awake()
    {
        Instance = this;
    }

    [Button]
    void TestGenerateMap()
    {
        //clear tiles
        while (tileContainer.childCount > 0)
        {
            DestroyImmediate(tileContainer.GetChild(0).gameObject);
        }
        //create tiles
        List<Vector2Int> slots = new List<Vector2Int>();
        for (int i = 0; i < row; i++)
        {
            List<Tile> rowTiles = new List<Tile>();
            for (int j = 0; j < col; j++)
            {
                GameObject clone = new GameObject("Tile");
                clone.transform.parent = tileContainer;
                clone.AddComponent<Tile>();
                clone.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                rowTiles.Add(clone.GetComponent<Tile>());
                slots.Add(new Vector2Int(i, j));
            }
            tiles.Add(rowTiles);
        }
        //set up piece and piece tracker
        for (int i = 0; i < 4; i++)
        {
            Vector2Int hor = slots[Random.Range(0, slots.Count)];
            slots.Remove(hor);
            Vector2Int ver = slots[Random.Range(0, slots.Count)];
            slots.Remove(ver);
            Vector2Int book = new Vector2Int(hor.x, ver.y);
            slots.Remove(book);

            tiles[hor.x][hor.y].SetUp(TileType.PIECE_TRACKER_HORIZONTAL, Random.Range(1, 3));
            tiles[ver.x][ver.y].SetUp(TileType.PIECE_TRACKER_VERTICAL, Random.Range(1, 3));
            tiles[book.x][book.y].SetUp(TileType.NONE, Random.Range(1, 3));//change to PIECE after 2 trackers found

            tiles[hor.x][hor.y].CreateBook((BookType)i);
            tiles[ver.x][ver.y].CreateBook((BookType)i);
            tiles[book.x][book.y].CreateBook((BookType)i);
        }
        //set up launchpad
        launchpadPos = slots[Random.Range(0, slots.Count)];
        slots.Remove(launchpadPos);
        tiles[launchpadPos.x][launchpadPos.y].SetUp(TileType.LAUNCH_PAD, 0);
        //set up tornado
        tornadoPos = slots[Random.Range(0, slots.Count)];
        slots.Remove(tornadoPos);
        tiles[tornadoPos.x][tornadoPos.y].SetUp(TileType.NONE, 0);
        tiles[tornadoPos.x][tornadoPos.y].CreateTornado();
        //set up oasis
        for (int i = 0; i < 4; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.OASIS, Random.Range(1, 3));
        }
        //set up gear
        GearDict gearDict = gameManager.GearDict;
        for (int i = 0; i < gearDict[GearType.DUNE_BLASTER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.DUNE_BLASTER);
        }
        for (int i = 0; i < gearDict[GearType.JET_PACK].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.JET_PACK);
        }
        for (int i = 0; i < gearDict[GearType.BOTTLE_OF_WATER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.BOTTLE_OF_WATER);
        }
        for (int i = 0; i < gearDict[GearType.SUNSCREEN].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.SUNSCREEN);
        }
        for (int i = 0; i < gearDict[GearType.TERRASCOPE].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.TERRASCOPE);
        }
        for (int i = 0; i < gearDict[GearType.CAPSULE].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.CAPSULE);
        }
        //set up other tiles
        for (int i=0;i<slots.Count;i++)
        {
            tiles[slots[i].x][slots[i].y].SetUp(TileType.NONE,Random.Range(1,3));
        }
    }

    public void GenerateMap()
    {
        //clear tiles
        while (tileContainer.childCount > 0)
        {
            DestroyImmediate(tileContainer.GetChild(0).gameObject);
        }
        //create tiles
        List<Vector2Int> slots = new List<Vector2Int>();
        for (int i = 0; i < row; i++)
        {
            List<Tile> rowTiles = new List<Tile>();
            for (int j = 0; j < col; j++)
            {
                GameObject clone = new GameObject("Tile");
                clone.transform.parent = tileContainer;
                clone.AddComponent<Tile>();
                clone.transform.position = new Vector3(i * tileDistance, 0, j * tileDistance);
                rowTiles.Add(clone.GetComponent<Tile>());
                slots.Add(new Vector2Int(i, j));
            }
            tiles.Add(rowTiles);
        }
        //set up piece and piece tracker
        for (int i = 0; i < 4; i++)
        {
            Vector2Int hor = slots[Random.Range(0, slots.Count)];
            slots.Remove(hor);
            Vector2Int ver = slots[Random.Range(0, slots.Count)];
            slots.Remove(ver);
            Vector2Int book = new Vector2Int(hor.x, ver.y);
            slots.Remove(book);

            tiles[hor.x][hor.y].SetUp(TileType.PIECE_TRACKER_HORIZONTAL, Random.Range(1, 3));
            tiles[ver.x][ver.y].SetUp(TileType.PIECE_TRACKER_VERTICAL, Random.Range(1, 3));
            tiles[book.x][book.y].SetUp(TileType.NONE, Random.Range(1, 3));//change to PIECE after 2 trackers found

            tiles[hor.x][hor.y].CreateBook((BookType)i);
            tiles[ver.x][ver.y].CreateBook((BookType)i);
            tiles[book.x][book.y].CreateBook((BookType)i);
        }
        //set up launchpad
        launchpadPos = slots[Random.Range(0, slots.Count)];
        slots.Remove(launchpadPos);
        tiles[launchpadPos.x][launchpadPos.y].SetUp(TileType.LAUNCH_PAD, 0);
        //set up tornado
        tornadoPos = slots[Random.Range(0, slots.Count)];
        slots.Remove(tornadoPos);
        tiles[tornadoPos.x][tornadoPos.y].SetUp(TileType.NONE, 0);
        tiles[tornadoPos.x][tornadoPos.y].CreateTornado();
        //set up oasis
        for (int i = 0; i < PhotonNetwork.room.MaxPlayers; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.OASIS, Random.Range(1, 3));
        }
        //set up gear
        GearDict gearDict = gameManager.GearDict;
        for (int i = 0; i < gearDict[GearType.DUNE_BLASTER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.DUNE_BLASTER);
        }
        for (int i = 0; i < gearDict[GearType.JET_PACK].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.JET_PACK);
        }
        for (int i = 0; i < gearDict[GearType.BOTTLE_OF_WATER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.BOTTLE_OF_WATER);
        }
        for (int i = 0; i < gearDict[GearType.SUNSCREEN].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.SUNSCREEN);
        }
        for (int i = 0; i < gearDict[GearType.TERRASCOPE].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.TERRASCOPE);
        }
        for (int i = 0; i < gearDict[GearType.CAPSULE].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateGear(GearType.CAPSULE);
        }
        //set up other tiles
        for (int i = 0; i < slots.Count; i++)
        {
            tiles[slots[i].x][slots[i].y].SetUp(TileType.NONE, Random.Range(1, 3));
        }
    }

    public void MoveTornado(Direction direction, int distance)
    {
        switch (direction)
        {
            case Direction.UP:
                {
                    while (distance > 0)
                    {
                        if ((tornadoPos.y + 1) > row)
                            break;
                        tiles[tornadoPos.x][tornadoPos.y].Layer = tiles[tornadoPos.x][tornadoPos.y + 1].Layer + 1;
                        tiles[tornadoPos.x][++tornadoPos.y].Layer = 0;
                        tiles[tornadoPos.x][tornadoPos.y].BlockType = BlockType.TORNADO;
                        distance--;
                    }
                }
                break;
            case Direction.DOWN:
                {
                    while (distance > 0)
                    {
                        if ((tornadoPos.y - 1) < 0)
                            break;
                        tiles[tornadoPos.x][tornadoPos.y].Layer = tiles[tornadoPos.x][tornadoPos.y - 1].Layer + 1;
                        tiles[tornadoPos.x][--tornadoPos.y].Layer = 0;
                        tiles[tornadoPos.x][tornadoPos.y].BlockType = BlockType.TORNADO;
                        distance--;
                    }
                }
                break;
            case Direction.LEFT:
                {
                    while (distance > 0)
                    {
                        if ((tornadoPos.x - 1) < 0)
                            break;
                        tiles[tornadoPos.x][tornadoPos.y].Layer = tiles[tornadoPos.x - 1][tornadoPos.y].Layer + 1;
                        tiles[--tornadoPos.x][tornadoPos.y].Layer = 0;
                        tiles[tornadoPos.x][tornadoPos.y].BlockType = BlockType.TORNADO;
                        distance--;
                    }
                }
                break;
            case Direction.RIGHT:
                {
                    while (distance > 0)
                    {
                        if ((tornadoPos.y + 1) > row)
                            break;
                        tiles[tornadoPos.x][tornadoPos.y].Layer = tiles[tornadoPos.x + 1][tornadoPos.y].Layer + 1;
                        tiles[++tornadoPos.x][tornadoPos.y].Layer = 0;
                        tiles[tornadoPos.x][tornadoPos.y].BlockType = BlockType.TORNADO;
                        distance--;
                    }
                }
                break;
        }
    }
}
