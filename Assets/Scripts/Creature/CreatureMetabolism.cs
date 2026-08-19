using UnityEngine;

public class CreatureMetabolism : MonoBehaviour
{
    private Creature creature;

    public void Initialize(Creature creature)
    {
        this.creature = creature;
    }

    public void Tick(float deltaTime)
    {
        creature.Stats.Age += deltaTime;

        creature.Stats.Energy = Mathf.Clamp01(creature.Stats.Energy);
    }

    public void ConsumeMovementEnegy(float distance)
    {
        float cost = distance * creature.Genes.Metabolism;

        ConsumeEnergy(cost);
    }

    public void AddEnergy(float amount)
    {
        creature.Stats.Energy += amount;
    }

    private void ConsumeEnergy(float amount)
    {
        creature.Stats.Energy -= amount;

        if (creature.Stats.Energy <= 0f)
        {
            creature.Stats.Energy = 0f;
            Die();
        }
    }

    private void Die()
    {
        if (!creature.Stats.IsAlive)
            return;

        creature.Stats.IsAlive = false;

        SimulationManager.Instance.Remove(creature);
    }
}
