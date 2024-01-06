using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    [SerializeField] PhotonView photonView;
    [SerializeField] SO_Player role;
    [SerializeField] List<MeshRenderer> parts = new List<MeshRenderer>();

    void SetUp()
    {
        foreach (MeshRenderer part in parts)
        {
            part.material = role.mat;
        }
    }

    private void Awake()
    {
        if (!photonView.isMine)
            return;
        SetUp();
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
