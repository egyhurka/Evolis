using UnityEngine;

public interface IConsumable : ISimulationEntity
{
    float Energy { get; }
}
