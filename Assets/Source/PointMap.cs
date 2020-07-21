using UnityEngine;
using System.Collections.Generic;

public class PointMap
{
  public List<Vector2> Points { get; set; }

  public int ChunkSize = 10;
  public int PointsPerChunk = 20;

  public List<(int, int)> Chunks;

  public PointMap()
  {
    Points = new List<Vector2>();
    Chunks = new List<(int, int)>();
  }

  public void Update(float x, float y)
  {
    var _x = Mathf.RoundToInt(x / ChunkSize);
    var _y = Mathf.RoundToInt(y / ChunkSize);

    UpdateChunk((_x, _y));


    UpdateChunk((_x + 1, _y));
    UpdateChunk((_x - 1, _y));
    UpdateChunk((_x, _y + 1));
    UpdateChunk((_x, _y - 1));

    UpdateChunk((_x + 1, _y + 1));
    UpdateChunk((_x + 1, _y - 1));
    UpdateChunk((_x - 1, _y + 1));
    UpdateChunk((_x - 1, _y - 1));
  }

  public void Clear()
  {
    Points.Clear();
    Chunks.Clear();
  }

  void UpdateChunk((int, int) chunk)
  {
    var finished = false;
    foreach (var ch in Chunks)
    {
      finished = finished ? finished : ch == chunk;
    }

    if (finished) return;

    var (x, y) = chunk;
    var o = ChunkSize / 2;

    Points.AddRange(AddPoints(PointsPerChunk, new Vector2(x * ChunkSize - o, y * ChunkSize - o), new Vector2(x * ChunkSize + o, y * ChunkSize + o)));
    Chunks.Add(chunk);
  }

  IEnumerable<Vector2> AddPoints(int count, Vector2 start, Vector2 end)
  {
    for (int i = 0; i < count; i++)
    {
      yield return new Vector2(Random.Range(start.x, end.x), Random.Range(start.y, end.y));
    }
  }
}
