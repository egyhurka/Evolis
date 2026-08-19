using UnityEngine;

public class SimpleBrain : MonoBehaviour, IBrain
{
    private Creature creature;

    private Vector3 wanderDirection;
    private float wanderTimer;

    private const float MinWanderTime = 1f;
    private const float MaxWanderTime = 4f;

    public void Initialize(Creature creature)
    {
        this.creature = creature;
    }

    public void Think()
    {
        if (creature.Stats.Energy < 0.5f)
        {
            FindFood();
        }
        else if (creature.Reproduction.IsReady)
        {
            FindPartner();
        }
        else
        {
            Wander();
        }
    }

    private void FindFood()
    {
        Food food = creature.Sensor.FindClosest<Food>();

        if (food == null)
        {
            Wander();
            return;
        }

        if (creature.Consumption.TryConsume(food))
        {
            creature.Movement.Stop();
            return;
        }

        Vector3 direction = food.Position - creature.Position;

        creature.Movement.Move(direction);
    }

    private void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
            ChooseWanderDirection();

        creature.Movement.Move(wanderDirection);
    }

    private void ChooseWanderDirection()
    {
        wanderDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        wanderTimer = Random.Range(MinWanderTime, MaxWanderTime);
    }

    private void FindPartner()
    {
        Creature partner = creature.Sensor.FindClosest<Creature>(other => creature.Reproduction.CanReproduceWith(other));

        if (partner == null)
        {
            Wander();
            return;
        }

        if (creature.Reproduction.TryReproduce(partner))
        {
            creature.Movement.Stop();
            return;
        }

        creature.Movement.Move(partner.Position - creature.Position);
    }
}
