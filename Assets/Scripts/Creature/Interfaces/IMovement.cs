using UnityEngine;

public interface IMovement
{
    Vector3 Velocity { get; }
    float DistanceMoved { get; }

    public void Initialize(Creature creature);
    public void Move(Vector3 direction);
    public void Stop();
}
