#pragma warning disable CS0649

using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private StageGrid grid;

    private Car car;
    private Vector2 touchDownPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchDownPosition = Input.mousePosition;
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0F, LayerMask.GetMask("Car"));
            car = hit.transform?.GetComponent<Car>() ?? car;
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
    }
}
