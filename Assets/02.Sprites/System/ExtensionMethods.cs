using System.Collections;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2Int ToDirection(this Vector2 vector)
    {
        if (Mathf.Abs(vector.y) > Mathf.Abs(vector.x))
        {
            return Vector2Int.RoundToInt(new Vector2(0F, vector.y));
        }
        return Vector2Int.RoundToInt(new Vector2(vector.x, 0F));
    }

    public static IEnumerator Start(this IEnumerator coroutine, MonoBehaviour behaviour)
    {
        behaviour.StartCoroutine(coroutine);
        return coroutine;
    }

    public static void Stop(this IEnumerator coroutine, MonoBehaviour behaviour)
    {
        behaviour.StopCoroutine(coroutine);
    }
}
