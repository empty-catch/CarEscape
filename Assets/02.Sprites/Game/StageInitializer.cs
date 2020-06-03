#pragma warning disable CS0649

using UnityEngine;

public class StageInitializer : MonoBehaviour
{
    [SerializeField]
    private Stage[] stages;
    [SerializeField]
    private CarGrid grid;

    private void Awake()
    {
        var currentStage = stages[Stage.Current];
        grid.Initialize(currentStage);
        currentStage.Initialize(grid);
    }
}
