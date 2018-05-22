using UnityEngine;
namespace SOPRO.Variables
{
    /// <summary>
    /// SO that holds a variable
    /// </summary>
    [CreateAssetMenu(fileName = "SOVariableVector3", menuName = "SOPRO/Variables/Vector3")]
    public class SOVariableVector3 : ScriptableObject
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
        public Vector3 Value;

        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(Vector3 value)
        {
            this.Value = value;
        }
        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(SOVariableVector3 value)
        {
            this.Value = value.Value;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(Vector3 amount)
        {
            this.Value += amount;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(SOVariableVector3 amount)
        {
            this.Value += amount.Value;
        }
		/// <summary>
        /// Conversion between variable to underlying value
        /// </summary>
        /// <param name="reference">variable to convert</param>
        public static implicit operator Vector3(SOVariableVector3 variable)
        {
            return variable.Value;
        }
    }
}
