using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    bool isExcavated = false;
    public bool IsExcavated
    {
        get => isExcavated;
        set => isExcavated=value;
    }
    [SerializeField] MeshRenderer water;

    public void Excavate()
    {
        isExcavated = true;
        water.material = TileManager.Instance.SandMat;
    }
}
