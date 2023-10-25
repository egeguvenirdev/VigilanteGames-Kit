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

    private PlayerManager playerManager;
    private HcLevelManager hcLevelManager;
    private MoneyManager moneyManager;
    private UIManager uIManager;

    void Start()
    {
        SetInits();
    }

    private void SetInits()
    {
        hcLevelManager = HcLevelManager.Instance;
        hcLevelManager.Init();

        moneyManager = MoneyManager.Instance;
        moneyManager.Init(clearPlayerPrefs);

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
