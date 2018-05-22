using UnityEngine;
public class EntityStats : MonoBehaviour, IStats
{
    private const int DefaultSubstituteHealthValue = 1;
    public float Speed { get { return speed; } set { speed = Mathf.Clamp(value, minSpeed, maxSpeed); } }
    public int Health
    {
        get
        {
            return health == null ? substituteHealth : health.NumberOfLives;
        }
        set
        {
            if (health == null)
            {
                substituteHealth = Mathf.Max(0, value);
                return;
            }

            int prevHealth = health.NumberOfLives;
            if (value < prevHealth)
                health.DecreaseLife(prevHealth - value);
            else if (value > prevHealth)
                health.IncreaseLife(value - prevHealth);
        }
    }
    [SerializeField]
    private float maxSpeed = 100f;
    [SerializeField]
    private float minSpeed = 5f;

    private float speed;

    private Health health;
    private int substituteHealth = DefaultSubstituteHealthValue;

    void Awake()
    {
        speed = minSpeed;
    }
    void Start()
    {
        health = GetComponent<Health>();
        if (health != null && substituteHealth != DefaultSubstituteHealthValue)
        {
            Health = substituteHealth;
            substituteHealth = DefaultSubstituteHealthValue;
        }
    }
}