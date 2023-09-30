using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private SimpleAnimancer animancer;
    private string currentAnimName;
    private string requestedAnimName;
    private bool canFire = true;

    public void PlayRunAnimation(float speed)
    {
        requestedAnimName = "Run";
        if (!CheckLastAnim("Run") || !canFire) return;
        currentAnimName = "Run";
        animancer.PlayAnimation("Run");
        animancer.SetStateSpeed(speed);
    }
    public void PlayIdleAnimation(float speed)
    {
        requestedAnimName = "Idle";
        if (!CheckLastAnim("Idle") || !canFire) return;
        currentAnimName = "Idle";
        animancer.PlayAnimation("Idle");
        animancer.SetStateSpeed(speed);
    }

    public void PlayFireAnimation(float speed)
    {
        if (canFire) StartCoroutine(Fire(speed));
    }

    private IEnumerator Fire(float speed)
    {
        if (requestedAnimName == "Run" && canFire)
        {
            currentAnimName = "FireRun";
            animancer.PlayAnimation("FireRun");
            animancer.SetStateSpeed(speed);

        }

        if (requestedAnimName == "Idle" && canFire)
        {
            currentAnimName = "FireIdle";
            animancer.PlayAnimation("FireIdle");
            animancer.SetStateSpeed(speed);
        }

        canFire = false;
        yield return new WaitForSeconds(0.480f);
        animancer.PlayAnimation(requestedAnimName);
        canFire = true;
        currentAnimName = requestedAnimName;
    }

    private bool CheckLastAnim(string animName)
    {
        if (animName != currentAnimName) return true;
        return false;
    }
}