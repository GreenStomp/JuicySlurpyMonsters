using System.Collections.Generic;
using UnityEngine;
using System;
namespace SOPRO 
{
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/Int")]
    [Serializable]
    public class SOEvInt : ScriptableObject
    {
				#if UNITY_EDITOR
		 				public int DEBUG_int_0 = default(int);
				#endif
		        [SerializeField]
        private readonly List<SOEvIntListener> listeners = new List<SOEvIntListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(int Value0)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvIntListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvIntListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
