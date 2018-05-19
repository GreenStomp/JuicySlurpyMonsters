using UnityEngine;
using UnityEditor;
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEvMesh))]
    public class SOEvMeshDrawer : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEvMesh e = target as SOEvMesh;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_Mesh_0);
        }
    }
