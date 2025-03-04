using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorButton : MonoBehaviour
{
    [Header("Ссылка на контроллер двери")]
    public DoorController doorController; // Присвойте здесь объект двери с DoorController

    // Метод, вызываемый при взаимодействии с кнопкой
    public void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (doorController != null)
        {
            doorController.ToggleDoor(); // Переключает состояние двери (открыть/закрыть)
        }
    }
}
