using System;
using UnityEngine;
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEvMeshMeshListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEvMeshMesh Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnEvMeshMesh Actions;

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
        internal void OnEventRaised(Mesh Value0, Mesh Value1)
        {
            Actions.Invoke(Value0, Value1);
        }
    }
