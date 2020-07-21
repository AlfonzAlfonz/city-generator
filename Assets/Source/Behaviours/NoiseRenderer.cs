using UnityEngine;

public class NoiseRenderer : MonoBehaviour
{
  public int width = 256;
  public int height = 256;

  public int layers = 3;
  public float lacunarity = 2;
  public float persistance = 2;

  public float scale = 10;

  public Vector2 offset = new Vector2(0,0);

  public AnimationCurve map = AnimationCurve.Linear(0, 0, 1, 1);

  TerrainNoise generator;

  void Start()
  {
    Render();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Render()
  {
    generator = new TerrainNoise(layers, lacunarity, persistance);

    var renderer = GetComponent<Renderer>();

    var texture = new Texture2D(width, height);
    texture.filterMode = FilterMode.Point;

    for (int x = 0; x < width; x++)
    {
      for (int y = 0; y < height; y++)
      {
        var h = generator.Get((float)(x + offset.x) / width * scale, (float)(y + offset.y) / height * scale);
        texture.SetPixel(x, y, Geo(map.Evaluate(h)));
      }
    }

    texture.Apply();

    renderer.sharedMaterial.mainTexture = texture;
  }

  Color BW(float h) => Color.Lerp(Color.black, Color.white, h);


  public Color green;
  public Color blue;

  public Color brown;

  public float waterLevel = 0.38f;


  Color Geo(float h) => h < waterLevel
    ? Color.Lerp(Color.black, blue, h / 0.4f)
    : Color.Lerp(green, brown, (h - 0.4f) / 0.6f);
}
