using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;

public class CamManager : MonoBehaviour
{
    [Header("Cam Follow Settings")]
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float playerFollowSpeed = 0.125f;
    [SerializeField] private float clampLocalX = 1.5f;

    [Header("Shake Settings")]
    [SerializeField] private MMCameraShaker camShaker;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeAmplitude;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private float shakeAmplitudeX;
    [SerializeField] private float shakeAmplitudeY;
    [SerializeField] private float shakeAmplitudeZ;
    [SerializeField] private bool unscaledTime;

    private Transform player;

    public void Init()
    {
        ActionManager.UpdateManager += OnUpdate;
        ActionManager.CamShake += OnCamShake;

        player = FindObjectOfType<PlayerManager>().GetCharacterTransform;
    }

    public void DeInit()
    {
        ActionManager.UpdateManager -= OnUpdate;
        ActionManager.CamShake -= OnCamShake;
    }

    private void OnUpdate(float deltaTime)
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + followOffset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -clampLocalX, clampLocalX);
            transform.position = Vector3.Lerp(transform.position, targetPosition, playerFollowSpeed * deltaTime);
        }
    }

    private void OnCamShake()
    {
        camShaker.ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency, shakeAmplitudeX, shakeAmplitudeY, shakeAmplitudeZ, unscaledTime);
    }
}