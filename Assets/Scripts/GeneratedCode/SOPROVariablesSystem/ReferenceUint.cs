using System;
using UnityEngine;
namespace SOPRO.Variables
{
    /// <summary>
    /// Class that holds a reference to a value
    /// </summary>
    [Serializable]
    public class ReferenceUint
    {
        /// <summary>
        /// Determines whenever reference should use a given value or a Variable value
        /// </summary>
        public bool UseConstant;
        /// <summary>
        /// Variable currently stored
        /// </summary>
        public SOVariableUint Variable;
        /// <summary>
        /// Current value
        /// </summary>
        public uint Value
        {
            get { return UseConstant ? constantValue : Variable.Value; }
			set
			{
				if (UseConstant)
				    constantValue = value;
				else
				    Variable.Value = value;
			}
        }

        [SerializeField]
        private uint constantValue;
        /// <summary>
        /// Construct a reference with default state
        /// </summary>
        public ReferenceUint()
        {
        }
        /// <summary>
        /// Construct reference given an initial value
        /// </summary>
        /// <param name="value"></param>
        public ReferenceUint(uint value)
        {
            UseConstant = true;
            constantValue = value;
        }
        /// <summary>
        /// Conversion between reference to underlying value
        /// </summary>
        /// <param name="reference">reference to convert</param>
        public static implicit operator uint(ReferenceUint reference)
        {
            return reference.Value;
        }
    }
}
