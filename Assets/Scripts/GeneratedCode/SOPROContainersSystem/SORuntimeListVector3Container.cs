using System;
using System.Collections.Generic;
using UnityEngine;
namespace SOPRO.Containers
{
	/// <summary>
    /// A non serializable class used to represent a shared container of objects
    /// </summary>
	[CreateAssetMenu(fileName = "RuntimeContainer", menuName = "SOPRO/Containers/SORuntimeListVector3Container")]
    public class SORuntimeListVector3Container : ScriptableObject
    {
        /// <summary>
        /// List of elements stored
        /// </summary>
		[NonSerialized]
        public List<Vector3> Elements = new List<Vector3>();
		        /// <summary>
        /// Get/Set element at the given index
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>value stored</returns>
        public Vector3 this[int i]
        {
            get { return Elements[i]; }
            set { Elements[i] = value; }
        }
		    }
}
