#pragma warning disable CS0649

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StageGrid : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private float spacing;
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private UnityEvent updateHeart;
    [SerializeField]
    private UnityEvent stageCleared;

    private Transform[,] slots;
    private Dictionary<Vector2Int, GridObject> objects = new Dictionary<Vector2Int, GridObject>();

    public Transform Transform(Vector2Int coordinate) => slots[coordinate.x, coordinate.y];

    public Vector2Int RandomCoordinate()
    {
        return new Vector2Int(2, 0);
    }

    public bool TryMoveCar(Vector2Int current, Vector2Int direction)
    {
        if (objects[current] is Car car)
        {
            var target = current + direction;
            var isExit = target == Stage.Exit;
            if ((isExit || CanMove(target, car)) && car.TryMove(direction, Transform(target), updateHeart))
            {
                if (objects.TryGetValue(target, out var gridObject) && gridObject is Heart)
                {
                    Heart.Count++;
                    updateHeart?.Invoke();
                    Destroy(gridObject.gameObject, 0.2F);
                    objects.Remove(target);
                }
                objects.Add(target, car);
                objects.Remove(current);

                if (isExit && car.Info.type == Car.Type.Red)
                {
                    Stage.Clear();
                    stageCleared?.Invoke();
                }
                return true;
            }
        }
        return false;
    }

    public void SetGridObject(GridObject gridObject, Vector2Int coordinate)
    {
        gridObject.transform.SetParent(Transform(coordinate), false);
        objects[coordinate] = gridObject;
    }

    public void Initialize()
    {
        float cellSize = (1F - spacing * (Stage.Size - 1)) / Stage.Size;
        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.spacing = new Vector2(spacing, spacing);
        slots = new Transform[Stage.Size, Stage.Size];

        for (int i = 0; i < Stage.Size * Stage.Size; i++)
        {
            var slot = Instantiate(slotPrefab, grid.transform).transform;
            slot.localScale = grid.cellSize;
            slots[i % Stage.Size, i / Stage.Size] = slot;
        }
    }

    private bool CanMove(Vector2Int coordinate, Car car)
    {
        var inOfRange = coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < Stage.Size && coordinate.y < Stage.Size;
        var carInOfRange = inOfRange &&
            (car.Info.axis == Axis.Horizontal ? coordinate.x + car.Info.length : coordinate.x) <= Stage.Size &&
            (car.Info.axis == Axis.Vertical ? coordinate.y + car.Info.length : coordinate.y) <= Stage.Size;
        var canMove = carInOfRange;

        for (int i = 0; i < car.Info.length && canMove; i++)
        {
            var checkDir = car.Info.axis == Axis.Horizontal ? Vector2Int.right : Vector2Int.up;
            canMove = IsEmpty(coordinate + checkDir * i, car);
        }
        return canMove;
    }

    private bool IsEmpty(Vector2Int coordinate, Car exception)
    {
        bool IsEmpty(Vector2Int direction)
        {
            for (int i = 0; i < Stage.LongestCarLength; i++)
            {
                var checkCoord = coordinate + direction * i;
                if (checkCoord.x >= 0 && checkCoord.y >= 0 && checkCoord.x < Stage.Size && checkCoord.y < Stage.Size &&
                    objects.ContainsKey(checkCoord) && objects[checkCoord] is Car car && car != exception &&
                    car.Info.length > i && car.Info.axis == (direction == Vector2Int.left ? Axis.Horizontal : Axis.Vertical))
                {
                    return false;
                }
            }
            return true;
        }
        return IsEmpty(Vector2Int.left) && IsEmpty(Vector2Int.down);
    }
}
