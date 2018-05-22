using UnityEngine;
namespace SOPRO.Variables
{
    /// <summary>
    /// SO that holds a variable
    /// </summary>
    [CreateAssetMenu(fileName = "SOVariableFloat", menuName = "SOPRO/Variables/Float")]
    public class SOVariableFloat : ScriptableObject
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
        /// Value stored in the variable
        /// </summary>
        public float Value;

        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(float value)
        {
            this.Value = value;
        }
        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(SOVariableFloat value)
        {
            this.Value = value.Value;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(float amount)
        {
            this.Value += amount;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(SOVariableFloat amount)
        {
            this.Value += amount.Value;
        }
		/// <summary>
        /// Conversion between variable to underlying value
        /// </summary>
        /// <param name="reference">variable to convert</param>
        public static implicit operator float(SOVariableFloat variable)
        {
            return variable.Value;
        }
    }
}
