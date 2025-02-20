using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    [Tooltip("Длительность анимации открытия двери (в секундах)")]
    public float openDuration = 0.5f; // Дверь откроется за 0.5 секунды

    [Tooltip("Коллайдер двери (если не назначен, попытается найти на объекте)")]
    public Collider doorCollider;

    [Tooltip("NavMeshObstacle двери (если используется)")]
    public NavMeshObstacle doorObstacle;

    void Start()
    {
        closedRotation = transform.rotation;
        // Поворот на 90° по Y относительно начальной ориентации
        openRotation = Quaternion.Euler(0, 90, 0) * closedRotation;

        // Если компоненты не назначены вручную, попробуем найти их
        if (doorCollider == null)
            doorCollider = GetComponent<Collider>();

        if (doorObstacle == null)
            doorObstacle = GetComponent<NavMeshObstacle>();
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            if (doorCollider != null)
                doorCollider.enabled = false; // Отключаем коллайдер, чтобы не блокировал путь
            if (doorObstacle != null)
                doorObstacle.enabled = false; // Отключаем препятствие, чтобы обновился NavMesh проем

            StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        float elapsedTime = 0f;
        while (elapsedTime < openDuration)
        {
            float t = elapsedTime / openDuration;
            transform.rotation = Quaternion.Lerp(closedRotation, openRotation, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = openRotation;
    }
}
