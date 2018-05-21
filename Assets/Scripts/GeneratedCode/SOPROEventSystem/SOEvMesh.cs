using System.Collections.Generic;
using UnityEngine;
using System;
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/Mesh")]
    [Serializable]
    public class SOEvMesh : ScriptableObject
    {
		#if UNITY_EDITOR
					public Mesh DEBUG_Mesh_0 = default(Mesh);
		#endif
	
        [SerializeField]
        private readonly List<SOEvMeshListener> listeners = new List<SOEvMeshListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(Mesh Value0)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvMeshListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvMeshListener listener)
        {
            listeners.Remove(listener);
        }
    }
