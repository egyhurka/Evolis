using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }

    private readonly List<ISimulationEntity> entities = new();

    private void Awake()
    {
        Instance = this;
    }

    public void Register(ISimulationEntity entity)
        => entities.Add(entity);

    public void Unregister(ISimulationEntity entity)
        => entities.Remove(entity);

    public void Remove(ISimulationEntity item)
    {
        Unregister(item);
        Destroy(item.GameObject);
    }

    public IEnumerable<T> GetAll<T>() where T : MonoBehaviour
    {
        return entities.OfType<T>();
    }
}
