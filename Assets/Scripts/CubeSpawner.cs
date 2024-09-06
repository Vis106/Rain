using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _delay;
    [SerializeField] private Zone _zone;
    [SerializeField] private ObjectPool _objectPool;

    private Coroutine _spawnCubeWithDelay;

    private void Start()
    {
        _objectPool.Initialize(_cubePrefab.gameObject);
        TurnOn();
    }

    private IEnumerator SpawningCube()
    {
        var timeInterval = new WaitForSeconds(_delay);

        while (_objectPool.TryGetObject(out GameObject cube))
        {
            yield return timeInterval;

            SpawnCube(cube, _zone.GetRandomPositionInZone());
        }
    }

    private void SpawnCube(GameObject cube, Vector3 spawnPoint)
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

        _spawnCubeWithDelay = StartCoroutine(SpawningCube());
    }
}
