using UnityEngine;
using UnityEditor;
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEvMeshMesh))]
    public class SOEvMeshMeshDrawer : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEvMeshMesh e = target as SOEvMeshMesh;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_Mesh_0 ,e.DEBUG_Mesh_1);
        }
    }
