using Entitas;

public class CreateEntitySystem : IInitializeSystem
{
    readonly Contexts contexts;

    public CreateEntitySystem(Contexts contexts)
    {
        this.contexts = contexts;
    }

    public void Initialize()
    {
        contexts.game.CreateEntity();
    }
}
