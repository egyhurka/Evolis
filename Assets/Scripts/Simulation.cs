using UnityEngine;

[RequireComponent(typeof(SimulationManager))]
[RequireComponent(typeof(Spawner))]
public class Simulation : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private Creature creaturePrefab;
    [SerializeField] private Food foodPrefab;

    [Header("Genes")]
    [SerializeField]
    private CreatureGenesPreset defaultGene;

    [Header("Population")]
    [SerializeField, Range(1, 1000)]
    private int startingCreatures = 50;

    [SerializeField, Range(1, 5000)]
    private int startingFood = 200;

    [Header("World")]
    [SerializeField]
    private Vector2 worldSize = new Vector2(50f, 50f);

    public static Simulation Instance { get; private set; }

    private void Start()
    {
        Instance = this;

        MakeWorld();

        SpawnInitialCreatures();
        SpawnInitialFood();
    }

    private void MakeWorld()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);

        ground.name = "Ground";
        ground.transform.position = Vector3.zero;

        ground.transform.localScale = new Vector3(worldSize.x / 10f, 1f, worldSize.y / 10f);
    }

    private void SpawnInitialCreatures()
    {
        for (int i = 0; i < startingCreatures; i++)
        {
            Creature creature = Spawner.Instance.SpawnRandom(creaturePrefab, worldSize);

            if (creature == null)
                continue;

            CreatureGenes genes = defaultGene.CreateGenes();

            if (i % 2 == 0)
            {
                genes.Color = Color.blue;
            }
            else
            {
                genes.Color = Color.red;
            }

            creature.Initialize(genes);
        }
    }

    public Creature SpawnCreature(Vector3 position, CreatureGenes genes)
    {
        Creature creature = Spawner.Instance.SpawnNear(creaturePrefab, position, 1.5f);

        if (creature == null)
            return default;

        creature.Initialize(genes);

        return creature;
    }

    private void SpawnInitialFood()
    {
        for (int i = 0; i < startingFood; i++)
        {
            Spawner.Instance.SpawnRandom(foodPrefab, worldSize);
        }
    }
}
