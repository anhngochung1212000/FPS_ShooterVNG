using Entitas;

public class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        Add(new CreateEntitySystem(contexts));
        Add(new InputSystem(contexts));
    }
}
