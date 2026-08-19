using System.Collections.Generic;
using UnityEngine;

public class CreatureSensor : MonoBehaviour
{
    private Creature creature;

    public void Initialize(Creature creature)
    { 
        this.creature = creature;
    }

    public T FindClosest<T>() where T : MonoBehaviour, ISenseable
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
}
