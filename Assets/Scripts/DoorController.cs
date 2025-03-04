using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Параметры двери")]
    public Transform doorTransform; // Перетащи сюда сам объект двери
    public float openAngle = 0f;    // Угол для открытой двери
    public float closedAngle = 90f; // Угол для закрытой двери
    public float doorSpeed = 2f;    // Скорость анимации

    private bool isOpen = false;

    // Делаем метод публичным, чтобы он был доступен из других скриптов
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        float targetAngle = isOpen ? openAngle : closedAngle;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(targetAngle));
    }

    private System.Collections.IEnumerator RotateDoor(float targetAngle)
    {
        Quaternion startRotation = doorTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * doorSpeed;
            doorTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }
    }
}
