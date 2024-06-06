using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvents : MonoBehaviour
{
    public static event Action OnLevelFinish;
    public static event Action<int> LoadNewLevel;
    public static void LevelFinish()
    {
        OnLevelFinish?.Invoke();
    }
    public static void LoadLevel(int levelNumber)
    {
        LoadNewLevel?.Invoke(levelNumber);
    }
}
