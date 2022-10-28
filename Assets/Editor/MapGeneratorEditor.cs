using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {
    //target is the object that the custom editor is inspecting
    // we want to cast that object to a map generator
    public override void OnInspectorGUI() {
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector ()) {
            //also generate the map on each change in "MapGenerator"
            if (mapGen.autoUpdate) {
                mapGen.GenerateMap ();
            }
        }
        //generate button
        if (GUILayout.Button ("Generate")) {
            mapGen.GenerateMap ();
        }
    }
}