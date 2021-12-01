//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity spawnEnemyPointEntity { get { return GetGroup(GameMatcher.SpawnEnemyPoint).GetSingleEntity(); } }
    public SpawnEnemyPointComponent spawnEnemyPoint { get { return spawnEnemyPointEntity.spawnEnemyPoint; } }
    public bool hasSpawnEnemyPoint { get { return spawnEnemyPointEntity != null; } }

    public GameEntity SetSpawnEnemyPoint(System.Collections.Generic.List<SWS.PathManager> newSpawnPointEnemy) {
        if (hasSpawnEnemyPoint) {
            throw new Entitas.EntitasException("Could not set SpawnEnemyPoint!\n" + this + " already has an entity with SpawnEnemyPointComponent!",
                "You should check if the context already has a spawnEnemyPointEntity before setting it or use context.ReplaceSpawnEnemyPoint().");
        }
        var entity = CreateEntity();
        entity.AddSpawnEnemyPoint(newSpawnPointEnemy);
        return entity;
    }

    public void ReplaceSpawnEnemyPoint(System.Collections.Generic.List<SWS.PathManager> newSpawnPointEnemy) {
        var entity = spawnEnemyPointEntity;
        if (entity == null) {
            entity = SetSpawnEnemyPoint(newSpawnPointEnemy);
        } else {
            entity.ReplaceSpawnEnemyPoint(newSpawnPointEnemy);
        }
    }

    public void RemoveSpawnEnemyPoint() {
        spawnEnemyPointEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SpawnEnemyPointComponent spawnEnemyPoint { get { return (SpawnEnemyPointComponent)GetComponent(GameComponentsLookup.SpawnEnemyPoint); } }
    public bool hasSpawnEnemyPoint { get { return HasComponent(GameComponentsLookup.SpawnEnemyPoint); } }

    public void AddSpawnEnemyPoint(System.Collections.Generic.List<SWS.PathManager> newSpawnPointEnemy) {
        var index = GameComponentsLookup.SpawnEnemyPoint;
        var component = (SpawnEnemyPointComponent)CreateComponent(index, typeof(SpawnEnemyPointComponent));
        component.spawnPointEnemy = newSpawnPointEnemy;
        AddComponent(index, component);
    }

    public void ReplaceSpawnEnemyPoint(System.Collections.Generic.List<SWS.PathManager> newSpawnPointEnemy) {
        var index = GameComponentsLookup.SpawnEnemyPoint;
        var component = (SpawnEnemyPointComponent)CreateComponent(index, typeof(SpawnEnemyPointComponent));
        component.spawnPointEnemy = newSpawnPointEnemy;
        ReplaceComponent(index, component);
    }

    public void RemoveSpawnEnemyPoint() {
        RemoveComponent(GameComponentsLookup.SpawnEnemyPoint);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherSpawnEnemyPoint;

    public static Entitas.IMatcher<GameEntity> SpawnEnemyPoint {
        get {
            if (_matcherSpawnEnemyPoint == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SpawnEnemyPoint);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSpawnEnemyPoint = matcher;
            }

            return _matcherSpawnEnemyPoint;
        }
    }
}
