#pragma warning disable CS0649

using System;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "Stage", menuName = "Scriptable Object/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField]
    private int size;
    [SerializeField]
    private int longestCarLength;
    [SerializeField]
    private Vector2Int exit;
    [SerializeField]
    private Vector2 exitOffset;
    [SerializeField]
    private Car.Information[] cars;

    public static bool AllCleared => Cleared == 0b0111;
    public static int Cleared => PlayerPrefs.GetInt("StageCleared", 0b0000);
    public static int Current { get; set; }
    public static int Size { get; private set; }
    public static int LongestCarLength { get; private set; }
    public static Vector2Int Exit { get; private set; }
    public static Vector2 ExitOffset { get; private set; }

    public static void Clear()
    {
        PlayerPrefs.SetInt("StageCleared", Cleared | 1 << Current);
    }

    public void Initialize()
    {
        Size = size;
        LongestCarLength = longestCarLength;
        Exit = exit;
        ExitOffset = exitOffset;
    }

    public void SpawnObjects(Heart heartPrefab, Car[] carPrefabs, Action<GridObject, Vector2Int> setGridObject, Func<Vector2Int> randomCoord)
    {
        foreach (var car in cars)
        {
            Spawn(car, carPrefabs, setGridObject);
        }
        Spawn(heartPrefab, setGridObject, randomCoord);
    }

    private void Spawn(Car.Information info, Car[] carPrefabs, Action<GridObject, Vector2Int> setGridObject)
    {
        float rotation = info.axis == Axis.Horizontal ? 0F : 90F;
        var car = Instantiate(carPrefabs[(int)info.type], Vector3.zero, Quaternion.Euler(0F, 0F, rotation));
        car.Info = info;
        setGridObject?.Invoke(car, info.coordinate);
    }

    private void Spawn(Heart heartPrefab, Action<GridObject, Vector2Int> setGridObject, Func<Vector2Int> randomCoord)
    {
        var heart = Instantiate(heartPrefab);
        setGridObject?.Invoke(heart, randomCoord?.Invoke() ?? Vector2Int.zero);
    }
}
