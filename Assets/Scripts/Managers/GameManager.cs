using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public static event Action<bool> GameOver;

    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    [Header("Money Settings")]
    [SerializeField] private int addMoney = 0;
    private float moneyMultiplier = 1;

    private PlayerManager playerManager;
    private HcLevelManager hcLevelManager;
    private UIManager uIManager;

    public float MoneyMultipler
    {
        get => moneyMultiplier;
        set => moneyMultiplier = value;
    }

    public int Money
    {
        get => PlayerPrefs.GetInt("TotalMoney", 0);
        set
        {
            float calculatedMoney = (float)value;
            if (value > 0)
            {
                calculatedMoney = (float)value * moneyMultiplier;
            }
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney", 0) + (int)calculatedMoney);
            UIManager.Instance.SetMoneyUI(Money, true);
        }
    }

    void Start()
    {
        if (clearPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();
            Money = addMoney;
        }

        if(Money <= 0) Money = addMoney;
        Money = 0;

        SetInits();
    }

    private void SetInits()
    {
        hcLevelManager = HcLevelManager.Instance;
        hcLevelManager.Init();

        uIManager = UIManager.Instance;
        uIManager.Init();

        playerManager = PlayerManager.Instance;
    }

    public void OnStartTheGame()
    {
        playerManager.Init();
    }

    public void OnLevelSucceed()
    {
        hcLevelManager.LevelUp();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLevelFailed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishTheGame(bool check)
    {
        playerManager.DeInit();
        uIManager.DeInit();

        if (check)
        {
            ActionManager.GameEnd?.Invoke(true);
            return;
        }
        ActionManager.GameEnd?.Invoke(false);
    }
}
