using UnityEngine;

public class Car : MonoBehaviour, IGridObject
{
    private Vector2Int coordinate;
    private int length;
    private RectTransform.Axis axis;

    public Vector2Int Coordinate { get; set; }

    public void Initialize(Vector2Int coordinate, int length, RectTransform.Axis axis)
    {
        this.coordinate = coordinate;
        this.length = length;
        this.axis = axis;
    }

    public void Execute()
    {

    }
}
