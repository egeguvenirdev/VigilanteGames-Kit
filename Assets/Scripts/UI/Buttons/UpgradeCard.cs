using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UpgradeCard : ButtonBase
{
    [Header("Button Settings")]
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private string buttonName;
    [SerializeField] private Button button;
    [SerializeField] private Color32 white;
    [SerializeField] private Color32 red;

    [Header("Button Prices")]
    [SerializeField] private int startPrice;
    [SerializeField] private int incrementalBasePrice;
    private int priceIncrementalValue;

    private int currentPrice;
    private int skillLevel;

    [Header("Button Upgrade Values")]
    [SerializeField] private float upgradeValue;
    [SerializeField] private float upgradeBaseIncrementalValue;
    private float upgradeIncrementalValue;

    private int upgradeCurrentValue;

    [Header("Panel Settings")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text priceText;

    private UIManager uiManager;
    private GameManager gameManager;
    private VibrationManager vibration;
    private PlayerManager playerManager;
    private int level;

    public override void Init()
    {
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
        vibration = VibrationManager.Instance;
        playerManager = PlayerManager.Instance;
        SetButtonText();
        SetButtonApperence();
        ApplyUpgrades();
    }

    public override void DeInit()
    {

    }

    public void OnPurchase()
    {
        SetButtonApperence();
        SetButtonText();
    }

    public override void OnButtonClick()
    {
        vibration.SelectionVibration();

        gameManager.Money = -CurrentPrice;
        SkillLevel = 1;
        SetButtons();
        playerManager.OnUpgrade(upgradeType, UpgradeCurrentValue);

        transform.DOKill(true);
        transform.DOScale(Vector3.one, 0);
        button.enabled = false;

        uiManager.UpgradeButtons();
        transform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 6).SetUpdate(true).OnComplete(() =>
        {

        });
    }

    public void SetButtons()
    {
        SetButtonPrice();
        SetUpgrades();
    }

    private void SetButtonPrice()
    {
        CurrentPrice = startPrice + incrementalBasePrice * (SkillLevel - 1);
        //CurrentPrice = startPrice + IncrementalPrice;
        //IncrementalPrice = IncrementalPrice;
    }

    private void SetUpgrades()
    {
        UpgradeIncrementalValue = upgradeBaseIncrementalValue * (SkillLevel - 1);
        UpgradeCurrentValue = upgradeValue + UpgradeIncrementalValue;
    }

    private void SetButtonApperence()
    {
        if (gameManager == null) gameManager = GameManager.Instance;

        if (gameManager.Money < CurrentPrice)
        {
            button.enabled = false;
            priceText.color = red;
        }
        else
        {
            button.enabled = true;
            priceText.color = white;
        }
    }

    private void SetButtonText()
    {
        if (uiManager == null) uiManager = UIManager.Instance;
        levelText.text = SkillLevel + " Lv";
        priceText.text = "" + uiManager.FormatFloatToReadableString(CurrentPrice);
        if (level == 0)
        {

            if (UpgradeCurrentValue == 0)
            {
                //upgradeStats.text = 0 + " -> " + upgradeValue;
                return;
            }

            //upgradeStats.text = uiManager.FormatFloatToReadableString(UpgradeCurrentValue) +
            //" -> " + uiManager.FormatFloatToReadableString(UpgradeCurrentValue + UpgradeIncrementalValue);
        }
    }

    public void ApplyUpgrades()
    {
        playerManager.OnUpgrade(upgradeType, UpgradeCurrentValue);
    }

    public int SkillLevel
    {
        get => PlayerPrefs.GetInt(buttonName + "skillLevel", 1);
        set => PlayerPrefs.SetInt(buttonName + "skillLevel", PlayerPrefs.GetInt(buttonName + "skillLevel", 1) + value);
    }

    public int CurrentPrice
    {
        get => PlayerPrefs.GetInt(buttonName + "currentPrice", startPrice);
        set => PlayerPrefs.SetInt(buttonName + "currentPrice", value);
    }

    public int IncrementalPrice
    {
        get => PlayerPrefs.GetInt(buttonName + "incrementalPrice", priceIncrementalValue);
        set => PlayerPrefs.SetInt(buttonName + "incrementalPrice", PlayerPrefs.GetInt(buttonName + "incrementalPrice", 0) + value);
    }

    public float UpgradeCurrentValue
    {
        get => PlayerPrefs.GetFloat(buttonName + "upgradeCurrentValue", upgradeValue);
        set => PlayerPrefs.SetFloat(buttonName + "upgradeCurrentValue", value);
    }

    public float UpgradeIncrementalValue
    {
        get => PlayerPrefs.GetFloat(buttonName + "upgradeIncrementalValue", upgradeIncrementalValue);
        set => PlayerPrefs.SetFloat(buttonName + "upgradeIncrementalValue", value);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(buttonName + "skillLevel");
        PlayerPrefs.DeleteKey(buttonName + "currentPrice");
        PlayerPrefs.DeleteKey(buttonName + "incrementalPrice");
        PlayerPrefs.DeleteKey(buttonName + "upgradeCurrentValue");
        PlayerPrefs.DeleteKey(buttonName + "upgradeIncrementalValue");
    }
}