using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Apple _prefabForSpawning;
    [SerializeField] private Transform _spawnZone;

    [SerializeField] private List<Vector3> _allPointsForSpawningInZone;

    private void Start()
    {
        FillPointsForSpawningList();
        Spawn();
    }

    public void Spawn()
    {
        Vector3 positionForSpawning = ReturnRandomSuitablePositionInSpawnZone();
        Apple newApple = Instantiate(_prefabForSpawning, positionForSpawning, Quaternion.identity, transform);
    }

    private void FillPointsForSpawningList()
    {
        Vector2 upperLeftCornerLocalPosition = new Vector2(-(_spawnZone.localScale.x / 2), (_spawnZone.localScale.y / 2));
        Vector2 upperRightCornerLocalPosition = new Vector2((_spawnZone.localScale.x / 2), (_spawnZone.localScale.y / 2));
        Vector2 lowerLeftCornerLocalPosition = new Vector2(-(_spawnZone.localScale.x / 2), (-_spawnZone.localScale.y / 2));
        Vector2 lowerRightCornerLocalPosition = new Vector2((_spawnZone.localScale.x / 2), (-_spawnZone.localScale.y / 2));

        for (int i = (int)upperLeftCornerLocalPosition.x; i < (int)upperRightCornerLocalPosition.x; i++)
        {
            for (int j = (int)upperLeftCornerLocalPosition.y; j < (int)lowerLeftCornerLocalPosition.y; j++)
                _allPointsForSpawningInZone.Add(new Vector3(i, j, transform.position.z) + _spawnZone.position);
        }
    }

    private Vector3 ReturnRandomSuitablePositionInSpawnZone()
    {
        System.Random random = new System.Random();
        List<Vector3> suitableForSpawningPoints = new List<Vector3>();
        Vector3 result;

        suitableForSpawningPoints = _allPointsForSpawningInZone.Except(_player.ReturnAllSnakePartsPositions()).ToList();
        result = suitableForSpawningPoints[random.Next(0, suitableForSpawningPoints.Count)];

        return result;
    }
}
