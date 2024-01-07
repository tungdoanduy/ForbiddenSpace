using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance {  get; private set; }

    [SerializeField] float tileDistance;
    public float TileDistance => tileDistance;
    [SerializeField] float tileHeight;
    public float TileHeight=>tileHeight;
    [SerializeField] int row, col;
    [SerializeField] Transform tileContainer;
    [SerializeField] List<List<Tile>> tiles = new List<List<Tile>>();
    public List<List<Tile>> Tiles => tiles;
    [SerializeField,ReadOnly] Vector2Int tornadoPos,portalPos;

    [SerializeField] GameObject sandTemplate;
    public GameObject SandTemplate => sandTemplate;
    [SerializeField] GameObject tornadoTemplate;
    public GameObject TornadoTemplate => tornadoTemplate;
    [SerializeField] GameObject bookTemplate;
    public GameObject BookTemplate => bookTemplate;
    [SerializeField] GameObject portalTemplate;
    public GameObject PortalTemplate => portalTemplate;
    [SerializeField] GameObject tunnelTemplate;
    public GameObject TunnelTemplate => tunnelTemplate;
    [SerializeField] GameObject oasisTemplate;
    public GameObject OasisTemplate => oasisTemplate;

    [SerializeField] Material sandMat;
    public Material SandMat => sandMat;

    private void Awake()
    {
        Instance = this;
    }

    //[Button]
    //void CreateLowerLayer()
    //{
    //    //clear tiles
    //    tiles.Clear();
    //    while (lowerTileContainer.childCount > 0)
    //    {
    //        DestroyImmediate(lowerTileContainer.GetChild(0).gameObject);
    //    }
    //    //create tiles
    //    for (int i = -extraLowerTiles; i < row + extraLowerTiles; i++)
    //    {
    //        for (int j = -extraLowerTiles; j < col + extraLowerTiles; j++)
    //        {
    //            GameObject sand = Instantiate(sandTemplate, new Vector3(i * tileDistance, 0, j * tileDistance), Quaternion.identity,lowerTileContainer);
    //        }
    //    }
    //}
    [PunRPC]
    public void GenerateMap()
    {
        //clear tiles
        tiles.Clear();
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

            tiles[hor.x][hor.y].BookType = (BookType)i;
            tiles[ver.x][ver.y].BookType = (BookType)i;
            tiles[book.x][book.y].BookType = (BookType)i;
        }
        //set up portal
        portalPos = slots[Random.Range(0, slots.Count)];
        slots.Remove(portalPos);
        tiles[portalPos.x][portalPos.y].SetUp(TileType.PORTAL, 0);
        tiles[portalPos.x][portalPos.y].CreatePortal();
        //set up tornado
        tornadoPos = slots[Random.Range(0, slots.Count)];
        slots.Remove(tornadoPos);
        tiles[tornadoPos.x][tornadoPos.y].SetUp(TileType.NONE, 0);
        tiles[tornadoPos.x][tornadoPos.y].BlockType = BlockType.TORNADO;
        tiles[tornadoPos.x][tornadoPos.y].CreateTornado();
        //set up oasis
        for (int i = 0; i < PhotonNetwork.room.MaxPlayers; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.WELL, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateOasis();
        }
        //set up tunnel
        for (int i = 0; i < PhotonNetwork.room.MaxPlayers; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.TUNNEL, Random.Range(1, 3));
            tiles[pos.x][pos.y].CreateTunnel();
        }
        //set up gear
        GearDict gearDict = GameManager.Instance.GearDict;
        for (int i = 0; i < gearDict[GearType.DUNE_BLASTER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].GearType = GearType.DUNE_BLASTER;
        }
        for (int i = 0; i < gearDict[GearType.TELEPORTER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].GearType = GearType.TELEPORTER;
        }
        for (int i = 0; i < gearDict[GearType.BOTTLE_OF_WATER].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].GearType = GearType.BOTTLE_OF_WATER;
        }
        for (int i = 0; i < gearDict[GearType.SOLAR_SHIELD].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].GearType = GearType.SOLAR_SHIELD;
        }
        for (int i = 0; i < gearDict[GearType.TERRASCOPE].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].GearType = GearType.TERRASCOPE;
        }
        for (int i = 0; i < gearDict[GearType.CAPSULE].quantity; i++)
        {
            Vector2Int pos = slots[Random.Range(0, slots.Count)];
            slots.Remove(pos);
            tiles[pos.x][pos.y].SetUp(TileType.GEAR, Random.Range(1, 3));
            tiles[pos.x][pos.y].GearType = GearType.CAPSULE;
        }
        //set up other tiles
        for (int i = 0; i < slots.Count; i++)
        {
            tiles[slots[i].x][slots[i].y].SetUp(TileType.NONE, Random.Range(1, 3));
        }
    }

    [PunRPC]
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
