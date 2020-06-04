#pragma warning disable CS0649

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Scriptable Object/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField]
    private int size;
    [SerializeField]
    private Vector2Int exit;
    [SerializeField]
    private CarInfo[] cars;
    [SerializeField]
    private HeartInfo[] hearts;

    public static int Current { get; set; }
    public int Size => size;

    public void Initialize(Action<Transform, Vector2Int> setGridObject)
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

    private void Spawn(CarInfo info, Action<Transform, Vector2Int> setGridObject)
    {
        float rotation = info.axis == RectTransform.Axis.Horizontal ? 0F : 90F;
        var car = Instantiate(info.prefab, Vector3.zero, Quaternion.Euler(0F, 0F, rotation));
        setGridObject?.Invoke(car.transform, info.coordinate);
        car.Initialize(info.coordinate, info.length, info.axis);
    }

    private void Spawn(HeartInfo info, Action<Transform, Vector2Int> setGridObject)
    {
        var heart = Instantiate(info.prefab).transform;
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
        public RectTransform.Axis axis;
    }
}
