using System;
using UnityEngine;
    /// <summary>
    /// Class that holds a reference to a value
    /// </summary>
    [Serializable]
    public class ReferenceString
    {
        /// <summary>
        /// Determines whenever reference should use a given value or a Variable value
        /// </summary>
        public bool UseConstant { get { return useConstant; } set { useConstant = variable; } }
        /// <summary>
        /// Variable currently stored
        /// </summary>
        public SOVariableString Variable { get { return variable; } set { variable = value; } }
        /// <summary>
        /// Current value
        /// </summary>
        public string Value
        {
            get { return useConstant ? constantValue : variable.Value; }
			set
			{
				if (useConstant)
				    constantValue = value;
				else
				    variable.Value = value;
			}
        }

        [SerializeField]
        private bool useConstant = true;
        [SerializeField]
        private string constantValue;
        [SerializeField]
        private SOVariableString variable;
        /// <summary>
        /// Construct a reference with default state
        /// </summary>
        public ReferenceString()
        {
        }
        /// <summary>
        /// Construct reference given an initial value
        /// </summary>
        /// <param name="value"></param>
        public ReferenceString(string value)
        {
            useConstant = true;
            constantValue = value;
        }
        /// <summary>
        /// Conversion between reference to underlying value
        /// </summary>
        /// <param name="reference">reference to convert</param>
        public static implicit operator string(ReferenceString reference)
        {
            return reference.Value;
        }
    }
