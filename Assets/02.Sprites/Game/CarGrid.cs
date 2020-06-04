#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class CarGrid : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private float spacing;
    [SerializeField]
    private GameObject slotPrefab;

    private Transform[,] slots;
    private Dictionary<Vector2Int, IGridObject> dictionary = new Dictionary<Vector2Int, IGridObject>();

    public Transform Transform(Vector2Int coordinate) => slots[coordinate.x, coordinate.y];

    public void Initialize(int size)
    {
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
}
