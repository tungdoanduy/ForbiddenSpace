using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField,ReadOnly] int layer = 1;
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
    List<GameObject> sands = new List<GameObject>();
    public List<GameObject> Sands => sands;
    [SerializeField, ReadOnly] TileType tileType;
    public TileType TileType
    {
        get => tileType;
        set => tileType = value;
    }
    [SerializeField, ReadOnly] Tunnel tunnel = null;
    public Tunnel Tunnel
    {
        get => tunnel;  
        set => tunnel = value;
    }
    [SerializeField,ReadOnly] BlockType blockType;
    public BlockType BlockType
    {
        get => blockType;
        set => blockType = value;
    }
    [SerializeField,ReadOnly] GearType gearType;
    public GearType GearType
    {
        get => gearType;
        set => gearType = value;
    }
    [SerializeField, ReadOnly] Gear gear = null;
    public Gear Gear { set => gear = value; }
    [SerializeField,ReadOnly] BookType bookType;
    public BookType BookType
    {
        get => bookType;
        set => bookType = value;
    }
    [SerializeField, ReadOnly] Book book = null;
    public Book Book { set => book = value; }

    public void SetUp(TileType tileType = TileType.NONE,int layer = 1)
    {
        this.tileType = tileType;
        this.Layer = layer;
        CreateSand(layer);
    }

    [PunRPC]
    public void CreateTornado()
    {
        GameObject tornado = Instantiate(TileManager.Instance.TornadoTemplate, transform.position, Quaternion.identity);
    }

    public void CreatePortal()
    {
        GameObject portal = Instantiate(TileManager.Instance.PortalTemplate, transform.position, Quaternion.identity,transform);
    }

    public void CreateTunnel()
    {
        GameObject tunnel = Instantiate(TileManager.Instance.TunnelTemplate, transform.position, Quaternion.identity,transform);
        this.tunnel = tunnel.GetComponent<Tunnel>();
    }

    public void CreateOasis()
    {
        GameObject oasis = Instantiate(TileManager.Instance.OasisTemplate, transform.position, Quaternion.identity, transform);
    }

    public void CreateBook(BookType bookType)
    {
        GameObject book = Instantiate(TileManager.Instance.BookTemplate, transform.position, Quaternion.identity, transform);
        BookInfo bookInfo = GameManager.Instance.BookDict[bookType];
        book.GetComponent<Book>().SetUp(bookInfo.coverMat, bookType, bookInfo.icon);
        this.book = book.GetComponent<Book>();
    }

    public void CreateSand( int layer)
    {
        for (int i = 0; i < layer; i++)
        {
            GameObject sand = Instantiate(TileManager.Instance.SandTemplate, transform.position + Vector3.up * i * TileManager.Instance.TileHeight, Quaternion.identity,transform);
            sands.Add(sand);
        }
    }

    [PunRPC]
    public void ClearSand()
    {
        Layer--;
        MeshRenderer currentSand = sands[sands.Count - 1].GetComponent<MeshRenderer>();
        sands.RemoveAt(sands.Count - 1);
        Material tempMat = Instantiate(TileManager.Instance.SandMat);
        currentSand.material = tempMat;
        float alpha = tempMat.GetFloat("_Alpha");
        DOTween.To(() => alpha, value => alpha = value, 0, 1).OnUpdate(() => tempMat.SetFloat("_Alpha", alpha)).OnComplete(()=>
        {
            Destroy(currentSand.gameObject);
            Destroy(tempMat);
        });
    }
}
