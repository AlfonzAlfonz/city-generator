using System.Collections.Generic;
using UnityEngine;

public class TriangulationGenerator
{

  public List<TriangleEdge> Edges { get; }

  public List<TriangleEdge> DoneEdges { get; }

  public List<Line> Voronoi { get; }

  float _minDistance;

  float _maxDistance;

  public TriangulationGenerator(Vector2 a, Vector2 b, Vector3 c, float minDistance = 1, float maxDistance = 3)
  {
    _minDistance = minDistance;
    _maxDistance = maxDistance;

    Edges = new List<TriangleEdge>();
    DoneEdges = new List<TriangleEdge>();
    Voronoi = new List<Line>();

    var cc = Circumcenter(a, b, c);

    Debug.Log(cc);

    Edges.Add(new TriangleEdge(b, c, cc));
    Edges.Add(new TriangleEdge(a, c, cc));
    Edges.Add(new TriangleEdge(a, b, cc));
  }

  public void Grow()
  {
    var edge = Edges[0];

    var c = RandomPoint(edge.root);

    while (!ValidPoint(c))
    {
      c = RandomPoint(edge.root);
    }

    var cc = Circumcenter(edge.a, edge.b, c);

    DoneEdges.Add(edge);
    Voronoi.Add(new Line(edge.circumcenter, cc));
    Edges.RemoveAt(0);

    Edges.Add(new TriangleEdge(edge.a, c, cc));
    Edges.Add(new TriangleEdge(edge.b, c, cc));
  }

  Vector2 RandomPoint(Vector2 root) => root + Random.insideUnitCircle * _maxDistance;

  bool ValidPoint(Vector2 point)
  {
    var valid = true;
    foreach (var edge in Edges)
    {
      valid = valid == false ? false : edge.innerCircleR <= Vector2.Distance(edge.circumcenter, point);
    }
    foreach (var edge in DoneEdges)
    {
      valid = valid == false ? false : edge.innerCircleR <= Vector2.Distance(edge.circumcenter, point);
    }
    return valid;
  }


  Vector2 Circumcenter(Vector2 a, Vector2 b, Vector3 c)
  {
    var ae = Vector2.Distance(b, c);
    var be = Vector2.Distance(a, c);
    var ce = Vector2.Distance(b, a);


    var aa = Mathf.Sin(2 * Angle(ae, be, ce));
    var ba = Mathf.Sin(2 * Angle(be, ce, ae));
    var ca = Mathf.Sin(2 * Angle(ce, ae, be));

    return new Vector2(
      (a.x * aa + b.x * ba + c.x * ca) / (aa + ba + ca),
      (a.y * aa + b.y * ba + c.y * ca) / (aa + ba + ca)
    );
  }

  float Angle(float a, float b, float c) => Mathf.Acos((b * b + c * c - a * a) / (2 * b * c));
}

public class TriangleEdge
{
  public readonly Vector2 a;
  public readonly Vector2 b;
  public readonly Vector2 circumcenter;

  public readonly float innerCircleR;
  public readonly Vector2 root;

  public TriangleEdge(Vector2 a, Vector2 b, Vector2 circumcenter)
  {
    this.a = a;
    this.b = b;
    this.circumcenter = circumcenter;
    this.innerCircleR = Vector2.Distance(a, circumcenter);
    this.root = Vector2.LerpUnclamped(Vector2.Lerp(a, b, 0.5f), circumcenter, -1f);
  }
}

public class Line
{
  public readonly Vector2 a;
  public readonly Vector2 b;

  public Line(Vector2 a, Vector2 b)
  {
    this.a = a;
    this.b = b;
  }
}