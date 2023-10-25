using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamFollower : MonoBehaviour
{
    [Header("Cam Follow Settings")]
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float playerFollowSpeed = 0.125f;
    [SerializeField] private float clampLocalX = 1.5f;

    private Transform player;

    public void Init()
    {
        DOTween.Init();
        player = PlayerManager.Instance.GetCharacterTransform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + followOffset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -clampLocalX, clampLocalX);
            transform.position = Vector3.Lerp(transform.position, targetPosition, playerFollowSpeed);
        }
    }
}