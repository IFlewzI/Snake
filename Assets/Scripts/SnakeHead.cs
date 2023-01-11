using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public void MoveTowards()
    {
        Player.Directions headDirection = GetComponentInParent<Player>().Direction;
        float speed = GetComponentInParent<Player>().Speed;

        switch (headDirection)
        {
            case Player.Directions.up:
                transform.position += Vector3.up * speed;
                break;
            case Player.Directions.right:
                transform.position += Vector3.right * speed;
                break;
            case Player.Directions.down:
                transform.position += Vector3.down * speed;
                break;
            case Player.Directions.left:
                transform.position += Vector3.left * speed;
                break;
        }
    }
}
