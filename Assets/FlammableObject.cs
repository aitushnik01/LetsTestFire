using UnityEngine;
using System.Collections;

public class FlammableObject : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How long the object burns (after fire starts) before the fire effect is removed.")]
    public float burnDuration = 5.0f;
    
    [Tooltip("Delay between catching fire and the fire effect actually starting.")]
    public float ignitionDelay = 1.0f;

    [Tooltip("The fire prefab to instantiate when ignited.")]
    public GameObject firePrefab;
    
    [Tooltip("If true, the object will be destroyed after burning.")]
    public bool destroyAfterBurn = true;
    
    [Tooltip("Offset from the object's position where the fire appears.")]
    public Vector3 fireOffset = new Vector3(0, 1f, 0);

    [Header("Debug")]
    [SerializeField] private bool isBurning = false;
    [SerializeField] private bool effectStarted = false; // Tracks when the fire effect has been spawned
    private float burnTimer = 0f;
    private GameObject fireInstance;

    /// <summary>
    /// Call this method to ignite the object.
    /// It starts a delay before the fire effect appears.
    /// </summary>
    public void Ignite()
    {
        if (isBurning)
            return;

        isBurning = true;
        Debug.Log($"🔥 [FlammableObject] {gameObject.name} is catching fire...");

        // Start the ignition delay coroutine.
        StartCoroutine(DelayedIgnite());
    }

    /// <summary>
    /// Waits for ignitionDelay seconds, then instantiates the fire effect.
    /// </summary>
    private IEnumerator DelayedIgnite()
    {
        yield return new WaitForSeconds(ignitionDelay);
        Debug.Log($"🔥 [FlammableObject] {gameObject.name} is now on fire!");

        if (firePrefab != null)
        {
            // Instantiate the fire effect as a child of the object.
            fireInstance = Instantiate(firePrefab, transform.position + fireOffset, Quaternion.identity, transform);
            Debug.Log($"🔥🔥 [FlammableObject] Fire spawned at {fireInstance.transform.position}");
        }
        else
        {
            Debug.LogError($"❌ [FlammableObject] {gameObject.name} - No firePrefab assigned!");
        }

        effectStarted = true;
    }

    void Update()
    {
        // Only update the burn timer after the fire effect has started.
        if (!isBurning || !effectStarted)
            return;

        burnTimer += Time.deltaTime;

        // Once the burn duration is reached, remove the fire effect and optionally destroy the object.
        if (burnTimer >= burnDuration)
        {
            if (fireInstance != null)
            {
                Destroy(fireInstance);
                Debug.Log($"🔥 [FlammableObject] Fire effect removed from {gameObject.name}");
            }

            if (destroyAfterBurn)
            {
                Destroy(gameObject);
                Debug.Log($"🔥 [FlammableObject] {gameObject.name} destroyed after burning.");
            }
            else
            {
                // Stop burning, reset the timer, and allow the object to remain.
                isBurning = false;
                effectStarted = false;
                burnTimer = 0f;
            }
        }
    }

    // Debug Gizmo: shows where the fire will be spawned.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + fireOffset, 0.2f);
    }
}
