using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SlideText : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private float lifeTime = 1;
    [SerializeField] private float valueY = 2f;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TextMeshPro tmpPro;

    private Color32 colorFade;
    private ObjectPooler pooler;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }

    public void SetTheText(int value, Color color, Transform parentObj, Vector3 pos)
    {
        text.text = value + " $";

        transform.parent = parentObj;
        transform.position = pos;
        colorFade = color;
        MovementAndColor();
        StartCoroutine(MovementAndColor());
    }

    private IEnumerator MovementAndColor()
    {
        colorFade.a = 255;
        transform.DOLocalMoveY(transform.localPosition.y + 0.5f, lifeTime).SetEase(Ease.OutQuint);
        DOTween.ToAlpha(() => colorFade, x => colorFade = x, 0, lifeTime).OnUpdate(() =>
        {
            tmpPro.color = colorFade;
        });

        yield return WaitForSecondsPooler.Wait(lifeTime);

        transform.parent = pooler.transform;
        gameObject.SetActive(false);
    }
}