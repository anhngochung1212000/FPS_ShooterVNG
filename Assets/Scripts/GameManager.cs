using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameManager : MonoBehaviour
{
    GameSystems gameSystems;

    void Start()
    {
        Contexts contexts = Contexts.sharedInstance;

        gameSystems = new GameSystems(contexts);
        gameSystems.Initialize();
    }

    void Update()
    {
        gameSystems.Execute();
    }
}
