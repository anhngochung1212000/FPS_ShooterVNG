using System.Collections;
using UnityEngine;
using Entitas;

public class CreateLocalPlayerSystem : IInitializeSystem
{
    readonly GameContext _gameContext;
    readonly SettingsContext _settingsContext;

    public CreateLocalPlayerSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _settingsContext = contexts.settings;
    }

    public void Initialize()
    {
        _gameContext.isLocalPlayer = true;
        var gameObject = Object.Instantiate(_settingsContext.gameSettings.value.playerSettings.playerPrefab);
        gameObject.transform.position = _gameContext.spawnPoint.position;
        gameObject.transform.rotation = _gameContext.spawnPoint.rotation;
    }
}
