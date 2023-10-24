using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonBase
{
    [SerializeField] private GameObject panelElements;
    [SerializeField] private GameObject upgradePanel;
    private GameManager gameManager;

    public override void Init()
    {
        gameManager = GameManager.Instance;
    }

    public override void DeInit()
    {

    }

    public override void OnButtonClick()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            return;
        }
        base.OnButtonClick();
        gameManager.OnStartTheGame();
        gameObject.SetActive(false);
    }
}
