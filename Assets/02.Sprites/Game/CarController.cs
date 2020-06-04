using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private CarGrid grid;
    private Transform controllingCar;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(touchPosition, Vector2.zero, 0F, LayerMask.GetMask("Car"));
            controllingCar = hit.transform ?? controllingCar;
        }
        else if (Input.GetMouseButtonUp(0) && controllingCar != null)
        {

        }
    }
}
