using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [Header("Components")]
    [SerializeField] private RunnerScript runnerScript;
    [SerializeField] private Transform characterTransform;

    private GameManager gameManager;

    Sequence sequence;

    public Transform GetCharacterTransform
    {
        get => characterTransform;
    }

    public void Init()
    {
        gameManager = GameManager.Instance;
        runnerScript.Init();
    }

    public void DeInit()
    {
        runnerScript.DeInit();
    }

    #region Upgrade
    public void OnUpgrade(UpgradeType type, float value)
    {
        switch (type)
        {
            case UpgradeType.Income:
                IncomeUpgrade(value);
                break;
            case UpgradeType.FireRange:
                FireRangeUpgrade(value);
                break;
            case UpgradeType.FireRate:
                FireRateUpgrade(value);
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }
    }

    private void IncomeUpgrade(float value)
    {
        if (gameManager == null) gameManager = GameManager.Instance;
        gameManager.MoneyMultipler = value;
    }

    private void FireRangeUpgrade(float value)
    {

    }

    private void FireRateUpgrade(float value)
    {

    }

    #endregion
}
