using UnityEngine;
using UnityEngine.AI;

public class SmoothNPCMovementDebug : MonoBehaviour
{
    [Header("Цели и объекты")]
    public Transform target;             // Точка назначения (например, за дверью)
    public Transform door;               // Объект двери (для триггера открытия)

    [Header("Параметры движения")]
    public float rotationSpeed = 5f;     // Скорость поворота NPC
    public float doorTriggerDistance = 3f; // Расстояние, на котором дверь открывается

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден!");
            return;
        }
        // Оставляем агенту обновлять позицию, но контролируем поворот вручную для плавности
        agent.updateRotation = false;
        agent.SetDestination(target.position);
    }

    void Update()
    {
        if (target == null)
            return;

        // Постоянно обновляем цель (на случай динамических изменений)
        agent.SetDestination(target.position);

        // Плавное вращение NPC в направлении движения
        Vector3 desiredVelocity = agent.desiredVelocity;
        if (desiredVelocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(desiredVelocity);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // Отрисовка пути (видно в Scene с включёнными Gizmos)
        if (agent.hasPath)
        {
            Vector3[] corners = agent.path.corners;
            for (int i = 0; i < corners.Length - 1; i++)
            {
                Debug.DrawLine(corners[i], corners[i + 1], Color.red);
            }
        }

        // Если объект двери назначен, проверяем расстояние до него и запускаем открытие
        if (door != null)
        {
            float d = Vector3.Distance(transform.position, door.position);
            if (d < doorTriggerDistance)
            {
                DoorController dc = door.GetComponent<DoorController>();
                if (dc != null)
                    dc.OpenDoor();
            }
        }
    }
}
