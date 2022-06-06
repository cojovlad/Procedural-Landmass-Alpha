using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //target is the object that the custom editor is inspecting
        // we want to cast that object to a map generator 
        MapGenerator mapGen = (MapGenerator)target;
        //if the drawing changes then autoupdate
       if( DrawDefaultInspector());
        {
            //also generate the map
            if(mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }
        if(GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}