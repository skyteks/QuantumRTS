using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawnUnit : MonoBehaviour
{
    [SerializeField]
    private EntityPrototypeAsset unityPrototype;

    private PlayerRef playerRef;

    public void Initialize(PlayerRef newPlayerRef)
    {
        playerRef = newPlayerRef;
    }

    public void SpawnUnit()
    {
        CommandSpawnUnit command = new CommandSpawnUnit()
        {
            unitPrototypeGUID = unityPrototype.Settings.Guid.Value,
        };
        QuantumRunner.Default.Game.SendCommand(command);
    }
}
