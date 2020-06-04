#pragma warning disable CS0649

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageGrid : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private float spacing;
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private Stage[] stages;

    private Transform[,] slots;
    private Dictionary<Vector2Int, GridObject> objects = new Dictionary<Vector2Int, GridObject>();

    public Transform Transform(Vector2Int coordinate) => slots[coordinate.x, coordinate.y];

    public bool TryMoveCar(Vector2Int current, Vector2Int direction)
    {
        if (objects[current] is Car car)
        {
            var target = current + direction;
            var inOfRange = target.x >= 0 && target.y >= 0 &&
                (car.Axis == Axis.Horizontal ? target.x + car.Length : target.x) <= Stage.Size &&
                (car.Axis == Axis.Vertical ? target.y + car.Length : target.y) <= Stage.Size;
            var canMove = inOfRange;

            for (int i = 0; i < car.Length && canMove; i++)
            {
                var checkDir = car.Axis == Axis.Horizontal ? Vector2Int.right : Vector2Int.up;
                canMove = IsEmpty(target + checkDir * i, car);
            }

            if (canMove && car.TryMove(direction, Transform(target)))
            {
                // TODO: target에 하트가 있으면 에러 발생
                objects.Add(target, car);
                objects.Remove(current);
                return true;
            }
        }
        return false;
    }

    private void Awake()
    {
        Action<GridObject, Vector2Int> setGridObject = (gridObject, coordinate) =>
        {
            gridObject.transform.SetParent(Transform(coordinate), false);
            objects[coordinate] = gridObject;
        };

        var currentStage = stages[Stage.Current];
        currentStage.Initialize();
        Initialize();
        currentStage.SpawnObjects(setGridObject);
    }

    private void Initialize()
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

    private bool IsEmpty(Vector2Int coordinate, Car exception)
    {
        bool IsEmpty(Vector2Int direction)
        {
            for (int i = 0; i < Stage.LongestCarLength; i++)
            {
                var checkCoord = coordinate + direction * i;
                if (checkCoord.x >= 0 && checkCoord.y >= 0 && checkCoord.x < Stage.Size && checkCoord.y < Stage.Size &&
                    objects.ContainsKey(checkCoord) && objects[checkCoord] is Car car && car != exception &&
                    car.Length > i && car.Axis == (direction == Vector2Int.left ? Axis.Horizontal : Axis.Vertical))
                {
                    return false;
                }
            }
            return true;
        }
        return IsEmpty(Vector2Int.left) && IsEmpty(Vector2Int.down);
    }
}
