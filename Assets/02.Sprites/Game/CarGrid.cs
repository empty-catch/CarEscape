#pragma warning disable CS0649

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

    public Transform this[Vector2Int coordinate] => this[coordinate.x, coordinate.y];
    public Transform this[int x, int y] => slots[x, y];

    public void Initialize(Stage stage)
    {
        float size = (1F - spacing * (stage.Size - 1)) / stage.Size;
        grid.cellSize = new Vector2(size, size);
        grid.spacing = new Vector2(spacing, spacing);
        slots = new Transform[stage.Size, stage.Size];

        for (int i = 0; i < stage.Size * stage.Size; i++)
        {
            var slot = Instantiate(slotPrefab, grid.transform).transform;
            slot.localScale = grid.cellSize;
            slots[i % stage.Size, i / stage.Size] = slot;
        }
    }
}
