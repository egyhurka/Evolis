using UnityEngine;

public class CreatureReproduction : MonoBehaviour
{
    private Creature creature;

    public bool IsReady => creature.Stats.Energy >= creature.Genes.ReproductionThreshold;

    public void Initialize(Creature creature)
    {
        this.creature = creature;
    }

    public bool CanReproduceWith(Creature other)
    {
        if (other == null)
            return false;

        if (other == creature)
            return false;

        if (!IsReady)
            return false;

        if (!other.Reproduction.IsReady)
            return false;

        return true;
    }

    public bool TryReproduce(Creature partner)
    {
        if (!CanReproduceWith(partner))
            return false;

        float distance = Vector3.Distance(creature.Position, partner.Position);

        if (distance > creature.Genes.ReproductionRange)
            return false;

        if (creature.Id > partner.Id)
            return false;

        ReproduceWith(partner);

        return true;
    }

    private void ReproduceWith(Creature partner)
    {
        CreatureGenes childGenes = GeneticsSystem.CreateChildGenes(creature.Genes, partner.Genes);

        Creature child = Simulation.Instance.SpawnCreature((creature.Position + partner.Position) / 2f, childGenes);

        if (child == null)
            return;

        float cost = creature.Genes.ReproductionCost;
        creature.Metabolism.ConsumeEnergy(cost);
        partner.Metabolism.ConsumeEnergy(cost);

        creature.Stats.Children++;
        partner.Stats.Children++;
    }
}
