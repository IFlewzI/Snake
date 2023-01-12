using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed => _speed;
    public Directions Direction => _direction;

    [SerializeField] private float _nextStepPause;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMoving;

    private float _timeSinceLastStep;
    private SnakeHead _head;
    private List<SnakeBodyPart> _otherBodyParts;
    private Vector3 _pointBehindSnake;
    private Directions _direction;
    private Directions _defaultDirection = Directions.right;
    
    public enum Directions
    {
        up,
        right,
        down,
        left
    }

    private void Awake()
    {
        _head = GetComponentInChildren<SnakeHead>();
        _otherBodyParts = new List<SnakeBodyPart>();
        _direction = _defaultDirection;

        for (int i = 0; i < transform.childCount - 1; i++)
            _otherBodyParts.Add(transform.GetChild(i + 1).gameObject.GetComponent<SnakeBodyPart>());
    }

    private void Update()
    {
        DirectionUpdate();
        _timeSinceLastStep += Time.deltaTime;

        if (_timeSinceLastStep >= _nextStepPause)
        {
            Move();
            _timeSinceLastStep = 0;
        }
    }

    public void GameOver()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void AddNewSnakePart()
    {
        SnakeBodyPart newSnakePart = Instantiate(_otherBodyParts[0], _pointBehindSnake, Quaternion.identity, transform);
        _otherBodyParts.Add(newSnakePart);
    }

    public List<Vector3> ReturnAllSnakePartsPositions()
    {
        List<Vector3> allSnakePartsPositions = new List<Vector3>();
        allSnakePartsPositions.Add(_head.transform.position);

        foreach (var snakePart in _otherBodyParts)
            allSnakePartsPositions.Add(snakePart.transform.position);

        return allSnakePartsPositions;
    }

    private void DirectionUpdate()
    {
        Directions newDirection;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            newDirection = Directions.up;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            newDirection = Directions.right;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            newDirection = Directions.down;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            newDirection = Directions.left;
        else
            newDirection = _direction;

        if (Math.Abs(newDirection - _direction) <= 1 || Math.Abs(newDirection - _direction) == 3)
            _direction = newDirection;
    }

    private void Move()
    {
        Vector3 previosBodyPartPosition = _head.GetComponent<Transform>().position;
        _head.MoveTowards();

        for (int i = 0; i < _otherBodyParts.Count; i++)
        {
            if (i > 0)
                previosBodyPartPosition = _otherBodyParts[i - 1].GetComponent<Transform>().position;

            if (i == _otherBodyParts.Count - 1)
                _pointBehindSnake = _otherBodyParts[i].GetComponent<Transform>().position;

            _otherBodyParts[i].SetNextPosition(previosBodyPartPosition);
        }

        foreach (var bodyPart in _otherBodyParts)
            bodyPart.MoveOnNextPosition();
    }
}
