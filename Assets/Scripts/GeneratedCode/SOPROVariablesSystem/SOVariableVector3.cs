using UnityEngine;
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
        /// Current variable value
        /// </summary>
        public Vector3 Value { get { return this.value; } set { this.value = value; } }
        [SerializeField]
        private Vector3 value;

        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(Vector3 value)
        {
            this.value = value;
        }
        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(SOVariableVector3 value)
        {
            this.value = value.Value;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(Vector3 amount)
        {
            this.value += amount;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(SOVariableVector3 amount)
        {
            this.value += amount.Value;
        }
    }
