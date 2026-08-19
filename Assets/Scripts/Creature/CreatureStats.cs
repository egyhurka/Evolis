using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    public float Energy;
    public float Age;

    public float DistanceTravelled;

    public int FoodEaten;
    public int Children;

    public bool IsAlive = true;

    private Creature creature;
    private Vector3 previousPosition;

    public void ResetStats()
    {
        Energy = 1f;
        Age = 0f;
        DistanceTravelled = 0f;
        FoodEaten = 0;
        Children = 0;
        IsAlive = true;
    }

    public void Initialize(Creature owner)
    {
        creature = owner;
        previousPosition = transform.position;
        ResetStats();
    }

    private void LateUpdate()
    {
        if (creature == null || !creature.IsInitialized || !IsAlive)
            return;

        Age += Time.deltaTime;

        float distanceMoved = Vector3.Distance(transform.position, previousPosition);
        previousPosition = transform.position;

        if (distanceMoved <= 0f)
            return;

        DistanceTravelled += distanceMoved;
        creature.Metabolism.ConsumeEnergy(distanceMoved * creature.Genes.Metabolism);
    }
}
