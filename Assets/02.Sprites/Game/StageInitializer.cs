#pragma warning disable CS0649

using UnityEngine;

public class StageInitializer : MonoBehaviour
{
    [SerializeField]
    private StageGrid grid;
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
        grid.Initialize();
        currentStage.SpawnObjects(heartPrefab, carPrefabs, grid.SetGridObject, grid.RandomCoordinate);
    }
}
