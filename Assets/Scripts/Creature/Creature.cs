using UnityEngine;

[RequireComponent(typeof(CreatureStats))]
[RequireComponent(typeof(CreatureMetabolism))]
[RequireComponent(typeof(CreatureSensor))]
[RequireComponent(typeof(CreatureConsumption))]
public class Creature : MonoBehaviour, ISimulationEntity
{
    [SerializeField]
    private CreatureGenes genes;

    [SerializeField]
    private CreatureStats stats;

    public CreatureGenes Genes => genes;
    public CreatureStats Stats => stats;

    public CreatureMetabolism Metabolism { get; private set; }
    public CreatureSensor Sensor { get; private set; }
    public CreatureConsumption Consumption { get; private set; }

    public IMovement Movement { get; private set; }
    public IBrain Brain { get; private set; }

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        Metabolism = GetComponent<CreatureMetabolism>();
        Sensor = GetComponent<CreatureSensor>();
        Consumption = GetComponent<CreatureConsumption>();

        Movement = GetComponent<IMovement>();
        Brain = GetComponent<IBrain>();

        if (Movement == null)
            Debug.LogError($"{name} has no IMovement component!");

        if (Brain == null)
            Debug.LogError($"{name} has no IBrain component!");
    }

    public void Initialize(CreatureGenes genes)
    {
        this.genes = genes;

        stats = new CreatureStats
        {
            Energy = 1f,
            Age = 0f
        };

        Movement.Initialize(this);
        Brain.Initialize(this);
        Metabolism.Initialize(this);
        Sensor.Initialize(this);
        Consumption.Initialize(this);
    }

    public void Update()
    {
        Brain.Think();

        Metabolism.Tick(Time.deltaTime);
        Metabolism.ConsumeMovementEnegy(Movement.DistanceMoved);
    }
}
