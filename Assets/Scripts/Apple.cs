using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Apple : MonoBehaviour
{
    [SerializeField] private UnityEvent _collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collected.Invoke();
        Destroy(gameObject);
    }
}
