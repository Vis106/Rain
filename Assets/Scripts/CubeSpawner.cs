using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : ObjectPool
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _delay;
    [SerializeField] private Zone _zone;

    private Coroutine _spawnCubeWithDelay;

    private void Start()
    {
        Initialize(_cubePrefab);
        TurnOn();
    }

    private IEnumerator SpawnCube()
    {
        var timeInterval = new WaitForSeconds(_delay);

        while (TryGetObject(out GameObject cube))
        {
            yield return timeInterval;

            SetCube(cube, _zone.GetRandomPositionInZone());
        }
    }

    private void SetCube(GameObject cube, Vector3 spawnPoint)
    {
        cube.SetActive(true);
        cube.transform.position = spawnPoint;
    }

    private void TurnOn()
    {
        if (_spawnCubeWithDelay != null)
        {
            StopCoroutine(_spawnCubeWithDelay);
        }

        _spawnCubeWithDelay = StartCoroutine(SpawnCube());
    }
}
