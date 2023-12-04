using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("PlayerPrefs")]
    [SerializeField] private bool clearPlayerPrefs;

    private PlayerManager playerManager;
    private LevelManager levelManager;
    private UpdateManager updateManager;
    private CamManager camManager;
    private MoneyManager moneyManager;
    private UIManager uIManager;
    private BandRotator bandRotator;

    void Start()
    {
        levelManager = LevelManager.Instance;
        moneyManager = MoneyManager.Instance;
        uIManager = UIManager.Instance;
        updateManager = FindObjectOfType<UpdateManager>();
        camManager = FindObjectOfType<CamManager>();
        

        SetInits();
    }

    private void SetInits()
    {
        levelManager.Init();
        uIManager.Init();
        moneyManager.Init(clearPlayerPrefs);
        updateManager.Init();
        camManager.Init();
    }

    private void DeInits()
    {
        levelManager.DeInit();
        uIManager.DeInit();
        updateManager.DeInit();
        camManager.DeInit();
        bandRotator.DeInit();
    }

    public void OnStartTheGame()
    {
        ActionManager.GameStart?.Invoke();

        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.Init();

        bandRotator = FindObjectOfType<BandRotator>();
        bandRotator.Init();
    }

    public void OnLevelSucceed()
    {
        levelManager.LevelUp();
        DeInits();
        SetInits();
    }

    public void OnLevelFailed()
    {
        DeInits();
        SetInits();
    }

    public void FinishTheGame(bool check)
    {
        playerManager.DeInit();

        if (check)
        {
            ActionManager.GameEnd?.Invoke(true);
            return;
        }
        ActionManager.GameEnd?.Invoke(false);
    }
}
