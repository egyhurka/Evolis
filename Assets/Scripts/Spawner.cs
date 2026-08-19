using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [SerializeField]
    private float spawnCheckRadius = 0.6f;

    [SerializeField]
    private int maxSpawnAttempts = 30;

    private void Awake()
    {
        Instance = this;
    }

    public T Spawn<T>(T prefab, Vector3 position) where T : MonoBehaviour
    {
        T instance = Instantiate(prefab, position, Quaternion.identity);

        if (instance is ISimulationEntity entity)
            SimulationManager.Instance.Register(entity);

        return instance;
    }

    public T SpawnRandom<T>(T prefab, Vector2 worldSize) where T : MonoBehaviour
    {
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector3 position = GetRandomPosition(worldSize);

            if (!IsPositionOccupied(position))
                return Spawn(prefab, position);
        }

        Debug.LogWarning($"Could not find spawn position for {typeof(T).Name}");

        return null;
    }

    public T SpawnNear<T>(T prefab, Vector3 origin, float radius) where T : MonoBehaviour
    {
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector2 offset = Random.insideUnitCircle * radius;
            Vector3 position = origin + new Vector3(offset.x, 0f, offset.y);
            position.y = 0.5f;

            if (!IsPositionOccupied(position))
                return Spawn(prefab, position);
        }

        Debug.LogWarning($"Could not find a nearby spawn position for {typeof(T).Name}");
        return null;
    }

    private Vector3 GetRandomPosition(Vector2 worldSize)
    {
        float x = Random.Range(-worldSize.x / 2f, worldSize.x / 2f);

        float z = Random.Range(-worldSize.y / 2f, worldSize.y / 2f);

        return new Vector3(x, 0.5f, z);
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        foreach (Collider collider in Physics.OverlapSphere(position, spawnCheckRadius))
        {
            if (collider.GetComponent<Creature>() != null || collider.GetComponent<Food>() != null)
                return true;
        }

        return false;
    }
}
