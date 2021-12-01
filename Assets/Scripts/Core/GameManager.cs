using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System;

public class GameManager : MonoBehaviour
{
    public static Action onReloadGameData;
    public GameSettingData gameSettings;

    Contexts _contexts;
    SettingsContext _settingsContext;
    GameSystem _gameSystem;

    void Awake()
    {
        _contexts = Contexts.sharedInstance;
        _settingsContext = _contexts.settings;
        _settingsContext.SetGameSettings(gameSettings);
        
    }

    void Start()
    {

        ConfigManager.InitGameData();
        _gameSystem = new GameSystem(_contexts);
        _gameSystem.Initialize();
        
    }

    void Update()
    {
        _gameSystem.Execute();
    }

    public static void ReloadGameData()
    {
        MissionControl.isVictory = false;
        MissionControl.CountEnemy = 0;
        onReloadGameData?.Invoke();
    }
}
