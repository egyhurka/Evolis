using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureSensor : MonoBehaviour
{
    private Creature creature;

    public void Initialize(Creature creature)
    { 
        this.creature = creature;
    }

    public T FindClosest<T>() where T : ISimulationEntity
    {
        IEnumerable<T> targets = SimulationManager.Instance.GetAll<T>();

        T closest = default;
        float closestDistance = float.MaxValue;

        foreach (T target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.Position);

            if (distance < closestDistance)
            { 
                closestDistance = distance;
                closest = target;
            }
        }

        return closest;
    }

    public T FindClosest<T>(System.Func<T, bool> condition) where T : ISimulationEntity
    {
        T closest = default;
        float closestDistance = float.MaxValue;

        foreach (T target in SimulationManager.Instance.GetAll<T>())
        {
            if (!condition(target))
                continue;

            float distance = Vector3.Distance(creature.Position, target.Position);

            if (distance < closestDistance)
            {
                closest = target;
                closestDistance = distance;
            }
        }

        return closest;
    }
}
