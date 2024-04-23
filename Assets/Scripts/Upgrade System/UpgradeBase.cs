using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBase : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] protected UpgradeInfosScriptable upgradeInfos;
    [SerializeField] private UpgradeType upgradeType;

    public UpgradeType UpgradeType { get => upgradeType; private set => upgradeType = value; }

    public void Init()
    {
        ActionManager.DistributePlayUpgradeValue?.Invoke(upgradeType, upgradeInfos.GetUpgradeInfos.CurrentValue);
    }

    public void DeInit()
    {

    }

    public float GetCurrentValue()
    {
        return upgradeInfos.GetUpgradeInfos.CurrentValue;
    }

    public virtual void OnUpgrade(UpgradeType upgradeType, float upgradeValue)
    {
        if (this.upgradeType == upgradeType)
        {
            upgradeInfos.GetUpgradeInfos.CurrentValue = upgradeValue;
            ActionManager.DistributePlayUpgradeValue?.Invoke(this.upgradeType, upgradeInfos.GetUpgradeInfos.CurrentValue);
        }
    }
}
