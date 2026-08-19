using UnityEngine;

[RequireComponent(typeof(CreatureStats))]
[RequireComponent(typeof(CreatureMetabolism))]
[RequireComponent(typeof(CreatureSensor))]
[RequireComponent(typeof(CreatureConsumption))]
[RequireComponent(typeof(CreatureReproduction))]
public class Creature : MonoBehaviour, ISimulationEntity
{
    [SerializeField]
    private CreatureGenes genes;

    private CreatureStats stats;

    public CreatureGenes Genes => genes;
    public CreatureStats Stats => stats;
    public bool IsInitialized => isInitialized;

    public CreatureMetabolism Metabolism { get; private set; }
    public CreatureSensor Sensor { get; private set; }
    public CreatureConsumption Consumption { get; private set; }
    public CreatureReproduction Reproduction { get; private set; }

    public IMovement Movement { get; private set; }
    public IBrain Brain { get; private set; }

    public int Id { get; private set; }

    private Renderer cRenderer;
    private MaterialPropertyBlock materialProperties;
    private bool isInitialized;

    public Vector3 Position => transform.position;
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        stats = GetComponent<CreatureStats>();
        if (stats == null)
            stats = gameObject.AddComponent<CreatureStats>();
        Metabolism = GetComponent<CreatureMetabolism>();
        Sensor = GetComponent<CreatureSensor>();
        Consumption = GetComponent<CreatureConsumption>();
        Reproduction = GetComponent<CreatureReproduction>();

        Movement = GetComponent<IMovement>();
        Brain = GetComponent<IBrain>();

        cRenderer = GetComponent<Renderer>();
        materialProperties = new MaterialPropertyBlock();

        if (Movement == null)
            Debug.LogError($"{name} has no IMovement component!");

        if (Brain == null)
            Debug.LogError($"{name} has no IBrain component!");

        if (cRenderer == null)
            Debug.LogError($"{name} has no Renderer component!");
    }

    private void Start()
    {
        if (!isInitialized)
            Initialize(genes ?? new CreatureGenes());
    }

    public void Initialize(CreatureGenes genes)
    {
        this.genes = genes;

        stats.Initialize(this);

        ApplyGenes();

        Movement.Initialize(this);
        Brain.Initialize(this);
        Metabolism.Initialize(this);
        Sensor.Initialize(this);
        Consumption.Initialize(this);
        Reproduction.Initialize(this);
        isInitialized = true;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    private void ApplyGenes()
    {
        materialProperties.SetColor("_BaseColor", genes.Color);
        cRenderer.SetPropertyBlock(materialProperties);

        transform.localScale = Vector3.one * genes.Size;
    }

    public void Update()
    {
        Brain.Think();

        Metabolism.Tick(Time.deltaTime);
    }
}
