#pragma warning disable CS0649

using System;
using UnityEngine;

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
    private CarInfo[] cars;
    [SerializeField]
    private HeartInfo[] hearts;

    public static int Current { get; set; }
    public static int Size { get; private set; }
    public static int LongestCarLength { get; private set; }
    public static Vector2Int Exit { get; private set; }

    public void Initialize()
    {
        Size = size;
        LongestCarLength = longestCarLength;
        Exit = exit;
    }

    public void SpawnObjects(Action<GridObject, Vector2Int> setGridObject)
    {
        foreach (var car in cars)
        {
            Spawn(car, setGridObject);
        }
        foreach (var heart in hearts)
        {
            Spawn(heart, setGridObject);
        }
    }

    private void Spawn(CarInfo info, Action<GridObject, Vector2Int> setGridObject)
    {
        float rotation = info.axis == Axis.Horizontal ? 0F : 90F;
        var car = Instantiate(info.prefab, Vector3.zero, Quaternion.Euler(0F, 0F, rotation));
        setGridObject?.Invoke(car, info.coordinate);
        car.Initialize(info.coordinate, info.length, info.axis);
    }

    private void Spawn(HeartInfo info, Action<GridObject, Vector2Int> setGridObject)
    {
        var heart = Instantiate(info.prefab);
        setGridObject?.Invoke(heart, info.coordinate);
    }

    [Serializable]
    private struct HeartInfo
    {
        public Heart prefab;
        public Vector2Int coordinate;
    }

    [Serializable]
    private struct CarInfo
    {
        public Car prefab;
        public Vector2Int coordinate;
        public int length;
        public Axis axis;
    }
}
