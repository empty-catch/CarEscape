#pragma warning disable CS0649

using UnityEngine;

public class StageInitializer : MonoBehaviour
{
    [SerializeField]
    private StageGrid grid;
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private Heart heartPrefab;
    [SerializeField]
    private Car[] carPrefabs;
    [SerializeField]
    private Stage[] stages;

    private void Awake()
    {
        var currentStage = stages[Stage.Current];
        currentStage.Initialize();
        grid.Initialize(slotPrefab);
        currentStage.SpawnObjects(heartPrefab, carPrefabs, grid.SetGridObject, grid.RandomCoordinate);

        var exit = Instantiate(slotPrefab).GetComponent<SpriteRenderer>();
        exit.transform.SetParent(grid.Transform(Stage.Exit), false);
        exit.transform.localPosition = Stage.ExitOffset;
        exit.color = new Color32(200, 40, 8, 255);
        exit.sortingOrder = -5;

        Heart.Count = 0;
    }
}
