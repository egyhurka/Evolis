using System;
using UnityEngine;

[Serializable]
public class CreatureGenes
{
    public float Speed = 2f;
    public float Size = 1f;

    public float VisionRange = 10f;
    public float VisionRadius = 90f;

    public float Metabolism = 0.005f;
    public float ReproductionThreshold = 0.8f;

    public float MutationRate = 0.05f;

    public float ConsumeRange = 0.75f;

    public float ReproductionRange = 2f;
    public float ReproductionCost = 0.25f;

    public Color Color = Color.white;

    public CreatureGenes Clone()
    {
        return (CreatureGenes)MemberwiseClone();
    }
}
