using System;
using UnityEngine;
    /// <summary>
    /// Class that holds a reference to a value
    /// </summary>
    [Serializable]
    public class ReferenceInt
    {
        /// <summary>
        /// Determines whenever reference should use a given value or a Variable value
        /// </summary>
        public bool UseConstant { get { return useConstant; } set { useConstant = variable; } }
        /// <summary>
        /// Variable currently stored
        /// </summary>
        public SOVariableInt Variable { get { return variable; } set { variable = value; } }
        /// <summary>
        /// Current value
        /// </summary>
        public int Value
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
        private int constantValue;
        [SerializeField]
        private SOVariableInt variable;
        /// <summary>
        /// Construct a reference with default state
        /// </summary>
        public ReferenceInt()
        {
        }
        /// <summary>
        /// Construct reference given an initial value
        /// </summary>
        /// <param name="value"></param>
        public ReferenceInt(int value)
        {
            useConstant = true;
            constantValue = value;
        }
        /// <summary>
        /// Conversion between reference to underlying value
        /// </summary>
        /// <param name="reference">reference to convert</param>
        public static implicit operator int(ReferenceInt reference)
        {
            return reference.Value;
        }
    }
