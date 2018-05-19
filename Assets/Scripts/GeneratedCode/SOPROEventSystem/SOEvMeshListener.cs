using System;
using UnityEngine;
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEvMeshListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEvMesh Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnEvMesh Actions;

        /// <summary>
        /// Adds listener to event
        /// </summary>
        protected virtual void OnEnable()
        {
            Event.AddListener(this);
        }
        /// <summary>
        /// Removes listener from event
        /// </summary>
        protected virtual void OnDisable()
        {
            Event.RemoveListener(this);
        }
        /// <summary>
        /// Invokes unity event
        /// </summary>
        internal void OnEventRaised(Mesh Value0)
        {
            Actions.Invoke(Value0);
        }
    }
