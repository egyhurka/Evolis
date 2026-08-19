using UnityEngine;

public static class GeneticsSystem
{
    public static CreatureGenes CreateChildGenes(CreatureGenes a, CreatureGenes b)
    {
        CreatureGenes child = new CreatureGenes
        {
            Speed = Pick(a.Speed, b.Speed),
            Size = Pick(a.Size, b.Size),

            VisionRange = Pick(a.VisionRange, b.VisionRange),
            VisionRadius = Pick(a.VisionRadius, b.VisionRadius),

            Metabolism = Pick(a.Metabolism, b.Metabolism),

            ReproductionThreshold = Pick(a.ReproductionThreshold, b.ReproductionThreshold),
            ReproductionRange = Pick(a.ReproductionRange, b.ReproductionRange),
            ReproductionCost = Pick(a.ReproductionCost, b.ReproductionCost),

            MutationRate = Pick(a.MutationRate, b.MutationRate),

            ConsumeRange = Pick(a.ConsumeRange, b.ConsumeRange),

            Color = Color.Lerp(a.Color, b.Color, Random.Range(0.25f, 0.75f))
        };

        Mutate(child);

        return child;
    }

    private static float Pick(float a, float b)
    {
        return Random.value < 0.5f ? a : b;
    }

    private static void Mutate(CreatureGenes genes)
    {
        float mutationRate = genes.MutationRate;

        genes.Speed = MutateValue(genes.Speed, mutationRate, 0.10f);
        genes.Size = MutateValue(genes.Size, mutationRate, 0.10f);

        genes.VisionRange = MutateValue(genes.VisionRange, mutationRate, 0.10f);
        genes.VisionRadius = MutateValue(genes.VisionRadius, mutationRate, 0.10f);

        genes.Metabolism = MutateValue(genes.Metabolism, mutationRate, 0.10f);

        genes.ReproductionThreshold = MutateValue(genes.ReproductionThreshold, mutationRate, 0.05f);
        genes.ReproductionRange = MutateValue(genes.ReproductionRange, mutationRate, 0.10f);
        genes.ReproductionCost = MutateValue(genes.ReproductionCost, mutationRate, 0.10f);

        genes.ConsumeRange = MutateValue(genes.ConsumeRange, mutationRate, 0.10f);

        genes.Color = MutateColor(genes.Color, mutationRate, 0.05f);

        genes.MutationRate = MutateValue(genes.MutationRate, mutationRate, 0.05f);

        ClampGenes(genes);
    }

    private static float MutateValue(float value, float mutationRate, float mutationStrength)
    {
        if (Random.value > mutationRate)
            return value;

        float change = Random.Range(-mutationStrength, mutationStrength);

        return value * (1f + change);
    }

    private static Color MutateColor(Color color, float mutationRate, float mutationStrength)
    {
        if (Random.value > mutationRate)
            return color;

        color.r += Random.Range(-mutationStrength, mutationStrength);
        color.g += Random.Range(-mutationStrength, mutationStrength);
        color.b += Random.Range(-mutationStrength, mutationStrength);

        color.r = Mathf.Clamp01(color.r);
        color.g = Mathf.Clamp01(color.g);
        color.b = Mathf.Clamp01(color.b);

        return color;
    }

    private static void ClampGenes(CreatureGenes genes)
    {
        genes.Speed = Mathf.Clamp(genes.Speed, 0.2f, 10f);
        genes.Size = Mathf.Clamp(genes.Size, 0.2f, 3f);

        genes.VisionRange = Mathf.Clamp(genes.VisionRange, 1f, 50f);
        genes.VisionRadius = Mathf.Clamp(genes.VisionRadius, 10f, 360f);

        genes.Metabolism = Mathf.Clamp(genes.Metabolism, 0.0001f, 0.1f);

        genes.ReproductionThreshold = Mathf.Clamp(genes.ReproductionThreshold, 0.1f, 1f);
        genes.ReproductionRange = Mathf.Clamp(genes.ReproductionRange, 0.1f, 5f);
        genes.ReproductionCost = Mathf.Clamp(genes.ReproductionCost, 0.01f, 1f);

        genes.MutationRate = Mathf.Clamp(genes.MutationRate, 0.001f, 0.5f);

        genes.ConsumeRange = Mathf.Clamp(genes.ConsumeRange, 0.1f, 3f);
    }
}