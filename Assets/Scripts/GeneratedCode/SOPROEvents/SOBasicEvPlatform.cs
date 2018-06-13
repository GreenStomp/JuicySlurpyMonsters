using System.Collections.Generic;
using UnityEngine;
using System;
using SOPRO;
namespace SOPRO
{
    /// <summary>
    /// Basic Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "BasicEvent", menuName = "SOPRO/BasicEvents/Platform")]
    [Serializable]
    public class SOBasicEvPlatform : BaseSOEvPlatform
    {
#if UNITY_EDITOR
        /// <summary>
        /// Description of the event, available only in UNITY_EDITOR
        /// </summary>
        [Multiline]
        [SerializeField]
        private string DEBUG_DeveloperDescription = "";
#endif

#if UNITY_EDITOR
        /// <summary>
        /// Debug field for inspector view, available only in UNITY_EDITOR
        /// </summary>
        public Platform DEBUG_Platform_0 = default(Platform);
#endif
        public delegate void SOBasicEvPlatformDel(Platform Value0);
        public event SOBasicEvPlatformDel Event;

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public override void Raise(Platform Value0)
        {
            if (Event != null)
                Event.Invoke(Value0);
        }
    }
}
