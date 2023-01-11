using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Border : MonoBehaviour
{
    [SerializeField] private UnityEvent _collision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hitted");
        _collision.Invoke();
    }
}
