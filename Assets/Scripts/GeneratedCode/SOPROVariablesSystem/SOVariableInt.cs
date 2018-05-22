using UnityEngine;
namespace SOPRO.Variables
{
    /// <summary>
    /// SO that holds a variable
    /// </summary>
    [CreateAssetMenu(fileName = "SOVariableInt", menuName = "SOPRO/Variables/Int")]
    public class SOVariableInt : ScriptableObject
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
        public int Value;

        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(int value)
        {
            this.Value = value;
        }
        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(SOVariableInt value)
        {
            this.Value = value.Value;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(int amount)
        {
            this.Value += amount;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(SOVariableInt amount)
        {
            this.Value += amount.Value;
        }
		/// <summary>
        /// Conversion between variable to underlying value
        /// </summary>
        /// <param name="reference">variable to convert</param>
        public static implicit operator int(SOVariableInt variable)
        {
            return variable.Value;
        }
    }
}
