using UnityEngine;
using UnityEditor;
namespace SOPRO.Events.Editor
{
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEvPlatform))]
    public class SOEvPlatformDrawer : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEvPlatform e = target as SOEvPlatform;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_Platform_0);
        }
    }
}
