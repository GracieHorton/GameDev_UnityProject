using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformMovement))]
[CanEditMultipleObjects]
public class PlatformMovementEditor : Editor {

    SerializedProperty points;

    private void OnEnable()
    {
        points = serializedObject.FindProperty("points");
    }

    public override void OnInspectorGUI()
    {

        this.DrawDefaultInspector();
        serializedObject.Update();
        if(GUILayout.Button("Add Waypoint"))
        {
            GameObject obj = new GameObject();
            Debug.Log(serializedObject.targetObject);
            obj.transform.SetParent((serializedObject.targetObject as PlatformMovement).transform);
            points.arraySize++;
            obj.name = serializedObject.targetObject.name + " " + points.arraySize;
            points.GetArrayElementAtIndex(points.arraySize - 1).objectReferenceValue = obj;
            
        }
        serializedObject.ApplyModifiedProperties();

    }
}
