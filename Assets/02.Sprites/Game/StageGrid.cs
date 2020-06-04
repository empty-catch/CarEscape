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

    private int size;
    private Transform[,] slots;
    private Dictionary<Vector2Int, GridObject> objects = new Dictionary<Vector2Int, GridObject>();

    public Transform Transform(Vector2Int coordinate) => slots[coordinate.x, coordinate.y];

    public bool TryMoveCar(Vector2Int current, Vector2Int direction)
    {
        if (objects[current] is Car car)
        {
            var target = current + direction;
            var inOfRange = target.x >= 0 && target.y >= 0 &&
                (car.Axis == Axis.Horizontal ? target.x + car.Length : target.x) <= size &&
                (car.Axis == Axis.Vertical ? target.y + car.Length : target.y) <= size;

            if (inOfRange && car.TryMove(direction, Transform(target)))
            {
                objects[target] = car;
                objects[current] = null;
                return true;
            }
        }
        return false;
    }

    public void Initialize(int size)
    {
        this.size = size;
        float cellSize = (1F - spacing * (size - 1)) / size;
        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.spacing = new Vector2(spacing, spacing);
        slots = new Transform[size, size];

        for (int i = 0; i < size * size; i++)
        {
            var slot = Instantiate(slotPrefab, grid.transform).transform;
            slot.localScale = grid.cellSize;
            slots[i % size, i / size] = slot;
        }
    }

    private void Awake()
    {
        Action<GridObject, Vector2Int> setGridObject = (gridObject, coordinate) =>
        {
            gridObject.transform.SetParent(Transform(coordinate), false);
            objects[coordinate] = gridObject;
        };

        var currentStage = stages[Stage.Current];
        Initialize(currentStage.Size);
        currentStage.Initialize(setGridObject);
    }
}
