using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HcLevelManager : MonoSingleton<HcLevelManager>
{
    [SerializeField] private GameObject[] levelPrefabs;
    private GameObject currentLevel;

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(ConstantVariables.LevelValue.Level);

        set => PlayerPrefs.SetInt(ConstantVariables.LevelValue.Level, PlayerPrefs.GetInt(ConstantVariables.LevelValue.Level) + value);
    }

    public void Init()
    {
        if (LevelIndex >= levelPrefabs.Length)
        {
            LevelIndex = Random.Range(0, levelPrefabs.Length);
        }

        GenerateCurrentLevel();
    }

    public void DeInit()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }

    public void GenerateCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
        currentLevel = Instantiate(levelPrefabs[LevelIndex]);
    }

    public void LevelUp()
    {
        LevelIndex = 1;

        if (LevelIndex >= levelPrefabs.Length)
        {
            LevelIndex = Random.Range(0, levelPrefabs.Length);
        }

        Init();
    }

    public int GetGlobalLevelIndex()
    {
        return LevelIndex;
    }
}
