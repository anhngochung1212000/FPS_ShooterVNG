using Entitas;
using SWS;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemySpawnPointSystem : IInitializeSystem
{
    readonly GameContext _gameContext;
    readonly List<PathManager> _spawPoints;

    public CreateEnemySpawnPointSystem(Contexts contexts, List<PathManager> spawPoints)
    {
        _gameContext = contexts.game;
        _spawPoints = spawPoints;
    }

    public void Initialize()
    {
        _gameContext.ReplaceSpawnEnemyPoint(_spawPoints);
    }
}
