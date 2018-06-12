using System;
using System.Collections.Generic;
using UnityEngine;
namespace SOPRO
{
	/// <summary>
    /// A class used to represent a shared container of objects
    /// </summary>
    [Serializable]
	[CreateAssetMenu(fileName = "Container", menuName = "SOPRO/Containers/SOLinkedListMeshFilterContainer")]
    public class SOLinkedListMeshFilterContainer : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// Description of the container, available only in UNITY_EDITOR
        /// </summary>
        [Multiline]
		[SerializeField]
        private string DEBUG_DeveloperDescription = "";
#endif
        /// <summary>
        /// List of elements stored
        /// </summary>
        public LinkedList<MeshFilter> Elements = new LinkedList<MeshFilter>();
		    }
}
