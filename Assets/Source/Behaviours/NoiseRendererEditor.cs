using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoiseRenderer))]
public class NoiseRendererEditor : Editor
{
  public override void OnInspectorGUI()
  {
    NoiseRenderer target = (NoiseRenderer)this.target;

    DrawDefaultInspector(); 

    if (GUILayout.Button("Generate"))
    {
      target.Render();
    }
  }
}
