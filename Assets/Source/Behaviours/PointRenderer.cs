using UnityEngine;

public class PointRenderer : MonoBehaviour
{
  public bool ShowEdgeVertices = true;
  public bool ShowEdges = true;
  public bool ShowCircumcenter = true;
  public bool ShowVoronoi = true;
  public bool ShowCircle = false;
  public bool ShowRoots = false;

  TriangulationGenerator _generator;
  TriangulationGenerator Generator
  {
    get
    {
      if (_generator == null)
      {
        _generator = new TriangulationGenerator(new Vector2(-2, -2), new Vector2(3, 0), new Vector2(0, 2));
      }
      return _generator;
    }
  }

  public void Add()
  {
    Generator.Grow();
  }

  public void Clear()
  {
    _generator = new TriangulationGenerator(new Vector2(-2, -2), new Vector2(3, 0), new Vector2(0, 2));
  }

  public void OnDrawGizmos()
  {

    foreach (var e in Generator.DoneEdges)
    {
      DrawEdge(e, true, false);
    }

    foreach (var e in Generator.Edges)
    {
      DrawEdge(e, false, e == Generator.Edges[0]);
    }

    foreach (var l in Generator.Voronoi)
    {
      Gizmos.color = Color.red;
      if (ShowVoronoi)
      {
        Gizmos.DrawLine(new Vector3(l.a.x, l.a.y), new Vector3(l.b.x, l.b.y));
      }
    }
  }

  void DrawEdge(TriangleEdge e, bool gray, bool first)
  {
    var c = gray ? Color.gray : Color.white;

    if (ShowEdgeVertices)
    {
      Gizmos.color = Color.white * c;

      DrawCube(e.a);
      DrawCube(e.b);
    }

    if (ShowEdges)
    {
      Gizmos.color = Color.white * c;
      if (first)
      {
        Gizmos.color = Color.red;
      }
      Gizmos.DrawLine(new Vector3(e.a.x, e.a.y), new Vector3(e.b.x, e.b.y));
    }

    if (ShowCircumcenter)
    {
      Gizmos.color = Color.blue * c;
      DrawCube(e.circumcenter, 0.05f);
    }

    if (ShowCircle)
    {
      Gizmos.color = Color.white * c;
      DrawCircle(e.circumcenter, e.innerCircleR);
    }

    if (ShowRoots)
    {
      Gizmos.color = Color.green * c;
      DrawCube(e.root, 0.05f);
      DrawCircle(e.root, 3);
    }
  }

  void DrawCube(Vector2 pos, float r = 0.1f) => Gizmos.DrawSphere(new Vector3(pos.x, pos.y), r);

  void DrawCircle(Vector2 pos, float r = 1) => Gizmos.DrawWireSphere(new Vector3(pos.x, pos.y), r);
}
