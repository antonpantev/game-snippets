using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapManager mm = (MapManager)target;
        if (GUILayout.Button("Build"))
        {
            mm.Build();
        }
    }
}
