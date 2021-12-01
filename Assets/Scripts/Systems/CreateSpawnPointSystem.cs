using Entitas;
using System.Collections.Generic;
using UnityEngine;
public class CreateSpawnPointSystem : IInitializeSystem
{
    readonly GameContext _gameContext;
    readonly Transform _spawPoints;

    public CreateSpawnPointSystem(Contexts contexts, Transform spawPoints)
    {
        _gameContext = contexts.game;
        _spawPoints = spawPoints;
    }

    public void Initialize()
    {
        _gameContext.ReplaceSpawnPoint(_spawPoints.position, _spawPoints.rotation);
    }
}
