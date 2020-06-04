using UnityEngine;
using DG.Tweening;

public class Car : GridObject
{
    private Vector2Int coordinate;
    private int length;
    private Axis axis;

    public Vector2Int Coordinate => coordinate;
    public int Length => length;
    public Axis Axis => axis;

    public bool TryMove(Vector2Int direction, Transform slot)
    {
        var canMove = CanMove(direction);
        if (canMove || Heart.Count > 0)
        {
            Heart.Count = canMove ? Heart.Count : Heart.Count - 1;
            Move(direction);
            transform.SetParent(slot, true);
            transform.DOMove(slot.position, 0.2F);
            return true;
        }
        return false;
    }

    private bool CanMove(Vector2Int direction)
    {
        var matchWhenHorizontal = axis == Axis.Horizontal && direction.y == 0;
        var matchWhenVertical = axis == Axis.Vertical && direction.x == 0;
        return matchWhenHorizontal || matchWhenVertical;
    }

    private void Move(Vector2Int direction)
    {
        this.coordinate += direction;
    }

    public void Initialize(Vector2Int coordinate, int length, Axis axis)
    {
        this.coordinate = coordinate;
        this.length = length;
        this.axis = axis;
    }
}
