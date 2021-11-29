using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input,Unique]
public class LookInputComponent : IComponent
{
    public Vector2 value;
}
