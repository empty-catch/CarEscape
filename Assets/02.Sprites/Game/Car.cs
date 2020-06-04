using System;
using UnityEngine;

public class Car : MonoBehaviour, IGridObject
{
    private Vector2Int coordinate;
    private int length;
    private RectTransform.Axis axis;

    public bool TryTranslate(Vector2Int direction, out Vector2Int coordinate)
    {
        var matchWhenHorizontal = axis == RectTransform.Axis.Horizontal && direction.y == 0;
        var matchWhenVertical = axis == RectTransform.Axis.Vertical && direction.x == 0;

        if (matchWhenHorizontal || matchWhenVertical)
        {
            this.coordinate += direction;
            coordinate = this.coordinate;
            return true;
        }

        coordinate = Vector2Int.zero;
        return false;
    }

    public void Initialize(Vector2Int coordinate, int length, RectTransform.Axis axis)
    {
        this.coordinate = coordinate;
        this.length = length;
        this.axis = axis;
    }

    public void Execute(Action action)
    {

    }
}
