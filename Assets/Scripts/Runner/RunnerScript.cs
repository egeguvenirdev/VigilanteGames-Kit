using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using DG.Tweening;

public class RunnerScript : MonoBehaviour
{
    [Header("Scripts and Transforms")]
    [SerializeField] private Transform model;
    [SerializeField] private Transform submodel;
    [SerializeField] private Transform localMoverTarget;
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private SimpleAnimancer animancer;
    [SerializeField] private PlayerSwerve playerSwerve;
    [SerializeField] private PathManager pathManager;

    [Header("Path Settings")]
    [SerializeField] private float distance = 0;
    [SerializeField] private float startDistance = 0;
    [SerializeField] private float clampLocalX = 2f;


    [Header("Run Settings")]
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float localTargetswipeSpeed = 2f;
    [SerializeField] private float swipeLerpSpeed = 2f;
    [SerializeField] private float swipeRotateLerpSpeed = 2f;

    private Vector3 oldPosition;
    private bool canRun = false;
    private bool canSwerve = false;
    private bool canFollow = true;
    private bool moveEnabled = false;
    private string currentAnimName = "Idle";

    public float RunSpeed
    {
        set => runSpeed = value;
    }

    public void Init()
    {
        ActionManager.SwerveValue += PlayerSwipe_OnSwerve;

        pathCreator = FindObjectOfType<PathCreator>();

        distance = startDistance;
        oldPosition = localMoverTarget.localPosition;

        //PlayAnimation("Run");
        StartToRun(true);
    }

    public void DeInit()
    {
        ActionManager.SwerveValue -= PlayerSwipe_OnSwerve;

        StartToRun(false);
    }

    void Update()
    {
        //UpdatePath();
        FollowLocalMoverTarget();
        oldPosition = model.localPosition;
    }

    private void StartToRun(bool checkRun)
    {
        if (checkRun)
        {
            canRun = true;
            canSwerve = true;
        }
        else
        {
            canRun = false;
            canSwerve = false;
        }
    }

    private void PlayerSwipe_OnSwerve(float direction)
    {
        if (canSwerve)
        {
            localMoverTarget.localPosition += Vector3.right * direction * localTargetswipeSpeed * Time.deltaTime;
            ClampLocalPosition();
        }
    }

    void ClampLocalPosition()
    {
        Vector3 pos = localMoverTarget.localPosition;
        pos.x = Mathf.Clamp(pos.x, -clampLocalX, clampLocalX);
        localMoverTarget.localPosition = pos;
    }

    void FollowLocalMoverTarget()
    {
        if (canRun && canFollow)
        {
            localMoverTarget.Translate(transform.forward * Time.deltaTime * runSpeed);

            Vector3 direction = localMoverTarget.localPosition - oldPosition;
            model.transform.forward = Vector3.Lerp(model.transform.forward, direction, swipeRotateLerpSpeed * Time.deltaTime);

            //swipe the object
            Vector3 nextPos = new Vector3(localMoverTarget.localPosition.x, model.localPosition.y, localMoverTarget.localPosition.z); ;
            model.localPosition = Vector3.Lerp(model.localPosition, nextPos, swipeLerpSpeed * Time.deltaTime);
        }
    }

    public void DodgeBack()
    {
        StartCoroutine(DodgeBackProcess());
    }

    IEnumerator DodgeBackProcess()
    {
        canSwerve = false;
        canRun = false;
        PlayAnimation("Knock", 1);

        yield return new WaitForSeconds(0.633f);

        PlayAnimation("Run", 1);
        canRun = true;
        canSwerve = true;
    }

    public void PlayAnimation(string animName)
    {
        animancer.PlayAnimation(animName);
        currentAnimName = animName;
    }

    public void PlayAnimation(string animName, float speed)
    {
        animancer.PlayAnimation(animName);
        animancer.SetStateSpeed(speed);
        currentAnimName = animName;
    }

    public void SwitchPathLine()
    {
        pathCreator = pathManager.ReturnCurrenntRoad();
        distance = 0;
    }

    private void OnGameEnd(bool winCondition)
    {
        DeInit();
        if (winCondition)
        {
            return;
        }
    }
}
