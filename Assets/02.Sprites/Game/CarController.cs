using UnityEngine;
using DG.Tweening;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private CarGrid grid;
    [SerializeField]
    private float minDragDistance;

    private Car controllingCar;
    private Vector2 touchDownPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchDownPosition = Input.mousePosition;
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0F, LayerMask.GetMask("Car"));
            controllingCar = hit.transform?.GetComponent<Car>() ?? controllingCar;
        }
        else if (Input.GetMouseButtonUp(0) && controllingCar != null)
        {
            var diffrence = ((Vector2)Input.mousePosition - touchDownPosition).normalized;
            var direction = diffrence.ToDirection();

            if (controllingCar.TryTranslate(direction, out var coordinate))
            {
                var transform = grid.Transform(coordinate);
                controllingCar.transform.SetParent(transform, true);
                controllingCar.transform.DOMove(transform.position, 0.2F);
            }
        }
    }
}
