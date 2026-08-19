using System;
using UnityEngine;

[Serializable]
public class CreatureGenes
{
    public float Speed;
    public float Size;

    public float VisionRange;
    public float VisionRadius;

    public float Metabolism;
    public float ReproductionThreshold;

    public float MutationRate;

    public float ConsumeRange;

    public Color Color;

    public CreatureGenes Clone()
    {
        return (CreatureGenes)MemberwiseClone();
    }
}