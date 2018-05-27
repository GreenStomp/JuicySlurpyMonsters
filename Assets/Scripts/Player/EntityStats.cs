using UnityEngine;
[RequireComponent(typeof(Health))]
public class EntityStats : MonoBehaviour
{
    public float Speed { get { return speed; } set { speed = Mathf.Clamp(value, minSpeed, maxSpeed); } }
    public int Health
    {
        get
        {
            return health.NumberOfLives;
        }
        set
        {
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

    void Awake()
    {
        health = GetComponent<Health>();
        speed = minSpeed;
    }
}