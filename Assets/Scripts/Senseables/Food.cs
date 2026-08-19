using UnityEngine;

public class Food : MonoBehaviour, IConsumable
{
    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    [field: SerializeField]
    public float Energy { get; private set; } = 0.4f;

}
