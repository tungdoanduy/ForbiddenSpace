using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] float appearHeight, disappearHeight;
    [SerializeField] Transform head;

    public void CloseTunnel()
    {
        float tempAngle=90;
        DOTween.To(() => tempAngle, value => tempAngle = value, 0, 1).OnUpdate(() =>
        {
            head.localRotation = Quaternion.Euler(0, 0, tempAngle);
            head.localPosition = new Vector3(-1.5f + 1.5f * Mathf.Cos(tempAngle * Mathf.Deg2Rad), 1.5f + 1.5f * Mathf.Sin(tempAngle * Mathf.Deg2Rad),0);
        }).SetEase(Ease.Linear);
    }
    public void OpenTunnel()
    {
        float tempAngle = 0;
        DOTween.To(() => tempAngle, value => tempAngle = value, 90, 1).OnUpdate(() =>
        {
            head.localRotation = Quaternion.Euler(0, 0, tempAngle);
            head.localPosition = new Vector3(-1.5f + 1.5f * Mathf.Cos(tempAngle * Mathf.Deg2Rad), 1.5f + 1.5f * Mathf.Sin(tempAngle * Mathf.Deg2Rad), 0);
        }).SetEase(Ease.Linear);
    }

    public void Appear()
    {
        transform.DOMoveY(appearHeight, 2).SetEase(Ease.Linear);
    }

    public void Disappear()
    {
        transform.DOMoveY(disappearHeight, 2).SetEase(Ease.Linear);
    }
}
