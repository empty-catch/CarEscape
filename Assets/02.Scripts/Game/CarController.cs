#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private StageGrid grid;

    private Car car;
    private IEnumerator getInput;
    private Vector2 touchDownPosition;

    public bool IsPaused
    {
        set
        {
            if (value)
            {
                getInput?.Stop(this);
            }
            else
            {
                getInput?.Start(this);
            }
        }
    }

    private void Awake()
    {
        getInput = GetInput().Start(this);
    }

    private IEnumerator GetInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchDownPosition = Input.mousePosition;
                var touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0F, LayerMask.GetMask("Car"));
                car = hit.transform?.GetComponent<Car>() ?? null;
            }
            else if (Input.GetMouseButtonUp(0) && car != null)
            {
                var diffrence = ((Vector2)Input.mousePosition - touchDownPosition);
                var direction = diffrence.normalized.ToDirection();

                if (direction != Vector2Int.zero)
                {
                    grid.TryMoveCar(car.Info.coordinate, direction);
                }
            }
            yield return null;
        }
    }
}
