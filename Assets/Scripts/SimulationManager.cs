using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }

    private readonly List<ISimulationEntity> entities = new();

    private int nextEntityId = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Register(ISimulationEntity entity)
    {
        if (entity is Creature creature)
        {
            creature.SetId(nextEntityId++);
        }

        entities.Add(entity);
    }

    public void Unregister(ISimulationEntity entity)
        => entities.Remove(entity);

    public void Remove(ISimulationEntity item)
    {
        Unregister(item);
        Destroy(item.GameObject);
    }

    public IEnumerable<T> GetAll<T>() where T : ISimulationEntity
    {
        return entities.OfType<T>();
    }
}
