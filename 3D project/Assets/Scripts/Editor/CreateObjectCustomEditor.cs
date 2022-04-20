//c# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateObject))]
[CanEditMultipleObjects]
public class CreateObjectCustomEditor : Editor
{
    SerializedProperty _createPointer;

    
    

    void OnEnable()
    {
        _createPointer = serializedObject.FindProperty("_createPointer");
    }

    public override void OnInspectorGUI()
    {
        
    }

    private void OnSceneGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseMove)
        {

        }
    }

}