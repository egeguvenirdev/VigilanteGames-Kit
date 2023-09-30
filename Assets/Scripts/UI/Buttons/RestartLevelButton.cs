using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RestartLevelButton : ButtonBase
{
    private GameManager gameManager;

    public override void Init()
    {
        ActionManager.GameEnd += OnGameEnd;
        gameManager = GameManager.Instance;
    }

    public override void DeInit()
    {
        ActionManager.GameEnd -= OnGameEnd;
    }

    private void OnGameEnd(bool check)
    {
        if (!check)
        {
            gameObject.SetActive(true);
        }
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        gameManager.OnLevelFailed();
        gameObject.SetActive(false);
    }
}
