using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Car : GridObject
{
    private Information info;
    public Information Info { get => info; set => info = value; }

    public bool TryMove(Vector2Int direction, Transform slot, UnityEvent updateHeart)
    {
        var canMove = CanMove(direction);
        if (canMove || Heart.Count > 0)
        {
            if (!canMove)
            {
                Heart.Count--;
                updateHeart?.Invoke();
            }
            info.Move(direction);
            transform.SetParent(slot, true);
            transform.DOMove(slot.position, 0.2F);
            return true;
        }
        return false;
    }

    private bool CanMove(Vector2Int direction)
    {
        var matchWhenHorizontal = Info.axis == Axis.Horizontal && direction.y == 0;
        var matchWhenVertical = Info.axis == Axis.Vertical && direction.x == 0;
        return matchWhenHorizontal || matchWhenVertical;
    }

    [Serializable]
    public struct Information
    {
        public Vector2Int coordinate;
        public int length;
        public Axis axis;
        public Type type;

        public void Move(Vector2Int direction)
        {
            coordinate += direction;
        }
    }

    public enum Type
    {
        Red, Blue, Green
    }
}
