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
    private LongObj[] cars;
    [SerializeField]
    private Obj[] hearts;

    public static int Current { get; set; }
    public int Size => size;

    public void Initialize(Func<Vector2Int, Transform> transform)
    {
        foreach (var car in cars)
        {
            Spawn(car, transform);
        }
        foreach (var heart in hearts)
        {
            Spawn(heart, transform);
        }
    }

    private void Spawn(Obj obj, Func<Vector2Int, Transform> transform)
    {
        var objTransform = obj.Spawn().transform;
        objTransform.SetParent(transform?.Invoke(obj.Coordinate), false);
    }

    [Serializable]
    private class Obj
    {
        [SerializeField]
        protected GameObject prefab;
        [SerializeField]
        private Vector2Int coordinate;

        public Vector2Int Coordinate => coordinate;

        public virtual GameObject Spawn()
        {
            return Instantiate(prefab);
        }
    }

    [Serializable]
    private class LongObj : Obj
    {
        [SerializeField]
        private int length;
        [SerializeField]
        private RectTransform.Axis axis;

        public override GameObject Spawn()
        {
            float rotation = axis == RectTransform.Axis.Horizontal ? 0F : 90F;
            return Instantiate(prefab, Vector3.zero, Quaternion.Euler(0F, 0F, rotation));
        }
    }
}
