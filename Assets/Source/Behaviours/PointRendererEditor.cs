using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointRenderer))]
public class PointRendererEditor : Editor
{
  public override void OnInspectorGUI()
  {
    PointRenderer target = (PointRenderer)this.target;

    DrawDefaultInspector(); 

    if (GUILayout.Button("Add"))
    {
      target.Add();
      UnityEditor.SceneView.RepaintAll();
      
    }

    if (GUILayout.Button("Clear"))
    {
      target.Clear();
      UnityEditor.SceneView.RepaintAll();
    }
  }
}
