using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //[SerializeField] List<MeshRenderer> parts = new List<MeshRenderer>();
    [SerializeField] MeshRenderer portal;
    [SerializeField] Material partMat;
    public void SetUp()
    {
        portal.gameObject.SetActive(false);
    }

    public void OpenPortal()
    {
        partMat.DOColor(Color.white, 2).OnComplete(()=> 
        {
            portal.gameObject.SetActive(true);
            //GameManager.Instance.Victory();
        });
        
    }
}
