using UnityEngine;

public class TerrainNoise
{
  float Layers { get; set; }
  float Lacunarity { get; set; }
  float Persistance { get; set; }

  public TerrainNoise(int layers = 3, float lacunarity = 2, float persistance = 2)
  {
    Layers = layers;
    Lacunarity = lacunarity;
    Persistance = 1 / persistance;
  }

  public float Get(float x, float y)
  {
    float val = 0;
    float d = 0;

    for (int i = 0; i < Layers; i++)
    {
      var f = Mathf.Pow(Persistance, i);

      var p = 1 / Mathf.Pow(Lacunarity, i);

      val += Mathf.PerlinNoise(x * f, y * f) * p;
      d += p;
    }

    return val / d;
  }
}
