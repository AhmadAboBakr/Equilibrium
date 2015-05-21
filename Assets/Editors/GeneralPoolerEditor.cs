#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
public struct pooledObjectData
{
    public int count;
    public GameObject pooledObject;
}
[CanEditMultipleObjects]
[CustomEditor(typeof(GeneralPooling))]
public class GeneralPoolerEditor : Editor
{
    int previousEnemyCount=1;
    int enemyTypes =1;

    GeneralPooling pooler;
    pooledObjectData[] objectsToPool;
    public override void OnInspectorGUI()
    {
        pooler = ((GeneralPooling)target);
        DrawDefaultInspector();
        enemyTypes = EditorGUILayout.IntField("EnemyTypes:", enemyTypes);
        if (previousEnemyCount != enemyTypes) {
            objectsToPool = new pooledObjectData[enemyTypes];
            previousEnemyCount = enemyTypes;
        }
        for (int i = 0; i < enemyTypes; i++)
        {
            GUILayout.BeginHorizontal("box");
            objectsToPool[i].pooledObject = (GameObject)EditorGUILayout.ObjectField("EnemyPrefab", (UnityEngine.GameObject)objectsToPool[i].pooledObject, typeof(GameObject), true);
            objectsToPool[i].count = EditorGUILayout.IntField(objectsToPool[i].count);
            GUILayout.EndHorizontal();

        }
        if (GUILayout.Button("Build Pooler"))
        {
            pooler.Build(objectsToPool);
        }
        if (GUILayout.Button("Clear Pooler"))
        {
            pooler.Clear();
        }


    }
}
#endif