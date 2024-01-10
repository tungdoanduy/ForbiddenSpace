using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    RectTransform rect;
    [SerializeField] List<Image> images = new List<Image>();
    [SerializeField] TMP_Text buttonText;
    [SerializeField] RectTransform sunContainer;
    Tween moveTween;

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.one*0.8f;
        foreach (Image image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
        }
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 0.5f);
    }
    public virtual void Enter()
    {
        rect.DOScale(1, 0.5f).SetUpdate(true);
        foreach(Image image in images)
        {
            image.DOFade(1, 0.5f).SetUpdate(true);
        }
        buttonText.DOFade(1, 0.5f).SetUpdate(true);
        moveTween.Kill();
        moveTween = sunContainer.DOAnchorPosX(0, sunContainer.anchoredPosition.x / -600 * 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            sunContainer.anchoredPosition = Vector2.left * 600;
            moveTween = sunContainer.DOAnchorPosX(0, 2).SetEase(Ease.Linear).OnComplete(() => sunContainer.anchoredPosition = Vector2.left * 600).SetUpdate(true).SetLoops(-1);
        });
    }

    public virtual void Exit()
    {
        rect.DOScale(0.8f, 0.5f);
        foreach (Image image in images)
        {
            image.DOFade(0.5f, 0.5f).SetUpdate(true);
        }
        buttonText.DOFade(0.5f, 0.5f).SetUpdate(true);
        moveTween.Kill();
        moveTween = sunContainer.DOAnchorPosX(sunContainer.anchoredPosition.x + 100, 2);
    }

    public virtual void Down()
    {
        rect.localScale = Vector3.one*0.8f;
    }

    public virtual void Up()
    {
        rect.localScale = Vector3.one;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
