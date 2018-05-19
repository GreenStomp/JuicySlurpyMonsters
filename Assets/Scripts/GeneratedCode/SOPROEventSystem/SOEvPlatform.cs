using System.Collections.Generic;
using UnityEngine;
using System;
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/Platform")]
    [Serializable]
    public class SOEvPlatform : ScriptableObject
    {
		#if UNITY_EDITOR
					public Platform DEBUG_Platform_0 = default(Platform);
		#endif
	
        [SerializeField]
        private readonly List<SOEvPlatformListener> listeners = new List<SOEvPlatformListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(Platform Value0)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvPlatformListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvPlatformListener listener)
        {
            listeners.Remove(listener);
        }
    }
