using System;
using UnityEngine;
namespace SOPRO.Events
{
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEvPlatformListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEvPlatform Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnEvPlatform Actions;

        /// <summary>
        /// Adds listener to event
        /// </summary>
        protected void RegisterToEvent()
        {
            Event.AddListener(this);
        }
        /// <summary>
        /// Removes listener from event
        /// </summary>
        protected void UnregisterToEvent()
        {
            Event.RemoveListener(this);
        }
				protected virtual void OnEnable()
        {
            Event.AddListener(this);
        }
		protected virtual void OnDisable()
        {
            Event.RemoveListener(this);
        }
		        /// <summary>
        /// Invokes unity event
        /// </summary>
        internal void OnEventRaised(Platform Value0)
        {
            Actions.Invoke(Value0);
        }
    }
}
