using UnityEngine;
using UnityEditor;
namespace SOPRO.Events.Editor
{
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEvInt))]
    public class SOEvIntDrawer : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEvInt e = target as SOEvInt;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_int_0);
        }
    }
}
