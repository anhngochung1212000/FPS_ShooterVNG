
using Entitas;
using UnityEngine;

public class InputSystem : IInitializeSystem,IExecuteSystem
{
    InputContext inputContext;

    public InputSystem(Contexts contexts)
    {
        inputContext = contexts.input;
    }

    public void Execute()
    {
        inputContext.ReplaceMoveInput(Vector2.zero);
        inputContext.ReplaceLookInput(Vector2.zero);
    }

    public void Initialize()
    {
        inputContext.SetMoveInput(Vector2.zero);
        inputContext.SetLookInput(Vector2.zero);
    }
}
