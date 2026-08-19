using UnityEngine;

public class CreatureConsumption : MonoBehaviour
{
    private Creature creature;

    public void Initialize(Creature creature)
    {
        this.creature = creature;
    }

    public bool TryConsume(IConsumable target)
    {
        if (target == null)
            return false;

        float distance = Vector3.Distance(
            creature.Position,
            target.Position
        );

        if (distance > creature.Genes.ConsumeRange)
            return false;

        creature.Metabolism.AddEnergy(target.Energy);

        SimulationManager.Instance.Remove(target);

        creature.Stats.FoodEaten++;
        return true;
    }
}
