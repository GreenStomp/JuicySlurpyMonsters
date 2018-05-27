using System;
using UnityEngine;
namespace SOPRO.Variables
{
    /// <summary>
    /// Class that holds a reference to a value
    /// </summary>
    [Serializable]
    public class ReferenceVector3
    {
        /// <summary>
        /// Determines whenever reference should use a given value or a Variable value
        /// </summary>
        public bool UseConstant { get { return useConstant; } set { useConstant = variable; } }
        /// <summary>
        /// Variable currently stored
        /// </summary>
        public SOVariableVector3 Variable { get { return variable; } set { variable = value; } }
        /// <summary>
        /// Current value
        /// </summary>
        public Vector3 Value
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
        private Vector3 constantValue;
        [SerializeField]
        private SOVariableVector3 variable;
        /// <summary>
        /// Construct a reference with default state
        /// </summary>
        public ReferenceVector3()
        {
        }
        /// <summary>
        /// Construct reference given an initial value
        /// </summary>
        /// <param name="value"></param>
        public ReferenceVector3(Vector3 value)
        {
            useConstant = true;
            constantValue = value;
        }
        /// <summary>
        /// Conversion between reference to underlying value
        /// </summary>
        /// <param name="reference">reference to convert</param>
        public static implicit operator Vector3(ReferenceVector3 reference)
        {
            return reference.Value;
        }
    }
}
