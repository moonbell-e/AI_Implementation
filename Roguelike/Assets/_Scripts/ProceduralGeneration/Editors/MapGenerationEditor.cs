using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap(Random.Range(0, 2147483647));
            }
        }

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap(Random.Range(0, 2147483647));
        }
    }
}
