using UnityEngine;
/// <summary>
/// SO that holds a variable
/// </summary>
[CreateAssetMenu(fileName = "SOVariablePlane", menuName = "SOPRO/Variables/Plane")]
public class SOVariablePlane : ScriptableObject
{
#if UNITY_EDITOR
    /// <summary>
    /// Description of the variable, available only in UNITY_EDITOR
    /// </summary>
    public string DEBUG_DeveloperDescription { get { return developerDescription; } }
    [Multiline]
    [SerializeField]
    private string developerDescription = "";
#endif
    /// <summary>
    /// Current variable value
    /// </summary>
    public Plane Value { get { return this.value; } set { this.value = value; } }
    [SerializeField]
    private Plane value;

    /// <summary>
    /// Sets value to given value
    /// </summary>
    /// <param name="value">new value</param>
    public void SetValue(Plane value)
    {
        this.value = value;
    }
    /// <summary>
    /// Sets value to given value
    /// </summary>
    /// <param name="value">new value</param>
    public void SetValue(SOVariablePlane value)
    {
        this.value = value.Value;
    }
    /// <summary>
    /// Increases value of given amount
    /// </summary>
    /// <param name="amount">increase amount</param>
    public void ApplyChange(Plane amount)
    {
        SetValue(amount);
    }
    /// <summary>
    /// Increases value of given amount
    /// </summary>
    /// <param name="amount">increase amount</param>
    public void ApplyChange(SOVariablePlane amount)
    {
        SetValue(amount);
    }
}
