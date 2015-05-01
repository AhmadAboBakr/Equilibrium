#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(Planet))]
public class AutoRotate : Editor
{
    private Planet Planetobj;


    public override void OnInspectorGUI()
    {
        Planetobj = ((Planet)target);
        DrawDefaultInspector();
        if (GUILayout.Button("Bake Planet"))
        {
            Planetobj.Build();
        }
    }
}
#endif  
