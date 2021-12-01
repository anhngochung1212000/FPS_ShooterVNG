using Entitas;

public class GameSystem : Feature
{
    public GameSystem(Contexts contexts)
    {
        Add(new CreateEntitySystem(contexts));
        Add(new CreateSpawnPointSystem(contexts,SpawCharacterControl.PosPlayer));
        Add(new CreateEnemySpawnPointSystem (contexts, MissionControl.PathEnemy));
        Add(new CreateEnemySystem(contexts));
        Add(new CreateLocalPlayerSystem(contexts));
    }
}
