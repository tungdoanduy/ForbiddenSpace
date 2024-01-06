using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> covers = new List<MeshRenderer>();
    [SerializeField] BookType bookType;

    public void SetUp(Material coverMat, BookType bookType)
    {
        foreach (MeshRenderer cover in covers) 
            cover.material = coverMat;
        this.bookType = bookType;
    }

}
