using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChunkRenderer))]
public class ChunkRendererEditor : Editor
{
  public override void OnInspectorGUI()
  {
    var target = (ChunkRenderer)this.target;

    if (DrawDefaultInspector())
    {
      target.Generator.Update(target.x, target.y);
      target.Calculator.CalculateDiagram(target.Generator.Points, ref target.diagram);
    }

    if(GUILayout.Button("Update")) {
      target.Generator.Update(target.x, target.y);
      target.Calculator.CalculateDiagram(target.Generator.Points, ref target.diagram);
    }

    if(GUILayout.Button("Clear")) {
      target.Generator.Clear();
      target.diagram = null;
    }
  }
}
