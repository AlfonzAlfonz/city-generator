using UnityEngine;
using Delaunay;

public class ChunkRenderer : MonoBehaviour
{
  public float x = 0;
  public float y = 0;

  PointMap _generator;
  public PointMap Generator
  {
    get
    {
      if (_generator == null)
      {
        _generator = new PointMap()
        {
          ChunkSize = 100,
          PointsPerChunk = 10
        };
      }
      return _generator;
    }
  }

  public VoronoiDiagram diagram;

  VoronoiCalculator _calculator;
  public VoronoiCalculator Calculator
  {
    get
    {
      if (_calculator == null)
      {
        _calculator = new VoronoiCalculator();
      }
      return _calculator;
    }
  }

  public void OnDrawGizmos()
  {
    Gizmos.color = new Color(0, 0, 0, 0.1f);

    foreach (var (x, y) in Generator.Chunks)
    {
      Gizmos.DrawCube(new Vector3(Generator.ChunkSize * x, Generator.ChunkSize * y), Generator.ChunkSize * Vector3.one);
    }

    if (diagram != null)
    {
      foreach (var l in diagram.Edges)
      {
        Gizmos.color = Color.black;
        if (l.Vert0 < 0 || l.Vert1 < 0)
        {
          continue;
        }
        var start = diagram.Vertices[l.Vert0];
        var end = diagram.Vertices[l.Vert1];
        Gizmos.DrawLine(start, end);
      }

      var trig = diagram.Triangulation;
      for (int i = 2; i < trig.Triangles.Count; i += 3)
      {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);

        var a = trig.Vertices[trig.Triangles[i]];
        var b = trig.Vertices[trig.Triangles[i - 1]];
        var c = trig.Vertices[trig.Triangles[i - 2]];

        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, a);
      }
    }

    foreach (var p in Generator.Points)
    {
      Gizmos.color = Color.cyan;
      Gizmos.DrawSphere(new Vector3(p.x, p.y), .1f);
    }



    Gizmos.color = Color.red;
    Gizmos.DrawSphere(new Vector3(x, y), 1);
  }
}
