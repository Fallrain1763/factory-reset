using UnityEngine;

public class DoorLatchController : MonoBehaviour
{
    public PressurePlate2D plateA;
    public PressurePlate2D plateB;
    public GameObject doorToDisable; // can be the door root or a visual/collider child
    public bool isOpen;

    private void OnEnable()
    {
        if (plateA) plateA.OnPressChanged += OnPlateChanged;
        if (plateB) plateB.OnPressChanged += OnPlateChanged;
        TryOpen();
    }

    private void OnDisable()
    {
        if (plateA) plateA.OnPressChanged -= OnPlateChanged;
        if (plateB) plateB.OnPressChanged -= OnPlateChanged;
    }

    private void OnPlateChanged(bool _) => TryOpen();

    private void TryOpen()
    {
        if (isOpen || plateA == null || plateB == null || doorToDisable == null) return;
        if (plateA.IsPressed && plateB.IsPressed)
        {
            isOpen = true;
            doorToDisable.SetActive(false); // permanently open
        }
    }
}
