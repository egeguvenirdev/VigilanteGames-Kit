using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class PlayerSwerve : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 1f;

    public event System.Action OnSwerveStart;
    public event System.Action<Vector2> OnSwerve;
    public event System.Action OnSwerveEnd;

    private bool touching;

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += LeanTouch_OnFingerDown;
        LeanTouch.OnFingerUpdate += LeanTouch_OnFingerSwerve;
        LeanTouch.OnFingerUp += LeanTouch_OnFingerUp;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= LeanTouch_OnFingerDown;
        LeanTouch.OnFingerUpdate -= LeanTouch_OnFingerSwerve;
        LeanTouch.OnFingerUp -= LeanTouch_OnFingerUp;
    }

    private void LeanTouch_OnFingerDown(LeanFinger obj)
    {
        OnSwerveStart?.Invoke();
        touching = true;
    }

    private void LeanTouch_OnFingerSwerve(LeanFinger finger)
    {
        if (!touching) return;
        OnSwerve?.Invoke(finger.ScaledDelta * _speedMultiplier);
    }

    private void LeanTouch_OnFingerUp(LeanFinger obj)
    {
        OnSwerveEnd?.Invoke();
        touching = false;
    }
}
