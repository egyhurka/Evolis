using UnityEngine;

[CreateAssetMenu(fileName = "CreatureGenesPreset", menuName = "Simulation/Creature Genes Preset")]
public class CreatureGenesPreset : ScriptableObject
{
    [Header("Movement")]
    public float Speed = 2f;
    public float Size = 1f;

    [Header("Senses")]
    public float VisionRange = 10f;
    public float VisionRadius = 90f;

    [Header("Metabolism")]
    public float Metabolism = 0.1f;
    public float ConsumeRange = 0.75f;

    [Header("Reproduction")]
    public float ReproductionThreshold = 0.8f;
    public float ReproductionRange = 2f;
    public float ReproductionCost = 0.25f;
    public float MutationRate = 0.05f;

    [Header("Appearance")]
    public Color Color = Color.white;

    public CreatureGenes CreateGenes()
    {
        return new CreatureGenes
        {
            Speed = Speed,
            Size = Size,

            VisionRange = VisionRange,
            VisionRadius = VisionRadius,

            Metabolism = Metabolism,
            ReproductionThreshold = ReproductionThreshold,
            ReproductionRange = ReproductionRange,
            ReproductionCost = ReproductionCost,

            MutationRate = MutationRate,

            ConsumeRange = ConsumeRange,

            Color = Color
        };
    }
}
