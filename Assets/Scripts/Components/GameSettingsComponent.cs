using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;
[Settings,Unique]
public class GameSettingsComponent : IComponent
{
    public GameSettingData value;
}
