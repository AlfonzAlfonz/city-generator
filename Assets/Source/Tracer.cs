using UnityEngine;

public static class Tracer
{
  public static T Trace<T>(this T arg) {
    Debug.Log(arg);
    return arg;
  }
}
