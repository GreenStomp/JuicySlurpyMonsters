using System.Collections.Generic;
using UnityEngine;
using System;
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/MeshMesh")]
    [Serializable]
    public class SOEvMeshMesh : ScriptableObject
    {
		#if UNITY_EDITOR
					public Mesh DEBUG_Mesh_0 = default(Mesh);
				public Mesh DEBUG_Mesh_1 = default(Mesh);
		#endif
	
        [SerializeField]
        private readonly List<SOEvMeshMeshListener> listeners = new List<SOEvMeshMeshListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(Mesh Value0, Mesh Value1)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0, Value1);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvMeshMeshListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvMeshMeshListener listener)
        {
            listeners.Remove(listener);
        }
    }
