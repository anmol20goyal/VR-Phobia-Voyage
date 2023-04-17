using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent collisionEvents;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Screw")
        {
            collisionEvents?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collisionEvents?.Invoke();
        }
    }
}
