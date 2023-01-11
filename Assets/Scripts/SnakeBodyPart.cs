using UnityEngine;

public class SnakeBodyPart : MonoBehaviour
{
    private Vector3 _nextPosition;

    public void MoveOnNextPosition()
    {
        transform.position = _nextPosition;
    }

    public void SetNextPosition(Vector3 position)
    {
        _nextPosition = position;
    }
}
