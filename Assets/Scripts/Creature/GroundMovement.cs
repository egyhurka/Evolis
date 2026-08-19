using UnityEngine;

public class GroundMovement : MonoBehaviour, IMovement
{
    private Creature creature;

    public Vector3 Velocity { get; private set; }

    public float DistanceMoved { get; private set; }

    public void Initialize(Creature creature)
    { 
        this.creature = creature;
    }

    public void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude <= 0.001f)
            return;

        float speed = creature.Genes.Speed;

        direction.y = 0f;
        direction.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.deltaTime);

        Velocity = direction * speed;

        Vector3 movement = Velocity * Time.deltaTime;

        transform.position += movement;

        DistanceMoved = movement.magnitude;
        creature.Stats.DistanceTravelled += DistanceMoved;
    }

    public void Stop()
    {
        Velocity = Vector3.zero;
    }
}
