using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using SWS;

[Game, Unique]
public class SpawnEnemyPointComponent : IComponent
{
    public List<PathManager> spawnPointEnemy;
}
