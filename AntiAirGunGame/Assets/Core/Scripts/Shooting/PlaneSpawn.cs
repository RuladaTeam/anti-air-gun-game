using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawn : MonoBehaviour
{
    [System.Serializable]
    private struct SpawnPoint
    {
        public GameObject group;
        public Transform spawnPointTransform;
    }

    [SerializeField, Min(1)] private int _maxSpawns;
    [SerializeField] private float ofsetBetweenSpawns;
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private float startOfsetBetweenSpawns;

    void Start()
    {
        startOfsetBetweenSpawns = ofsetBetweenSpawns;
    }

    private void Update()
    {
        if(_maxSpawns > _spawnPoints.Count)
        {
            _maxSpawns = _spawnPoints.Count;
        }
        if (ofsetBetweenSpawns > 0)
        {
            ofsetBetweenSpawns -= Time.deltaTime;
        }
        else
        {
            ofsetBetweenSpawns = startOfsetBetweenSpawns;
            Spawn();
        }
    }

    [ContextMenu("GetSpawn")]
    private void Spawn()
    {
        int countOfSpawn = Random.Range(1, _maxSpawns + 1);
        List<SpawnPoint> currentSpawnPoints = new List<SpawnPoint>(_spawnPoints);
        for(int i = 0; i < countOfSpawn; i++)
        {
            int currentSpawnIndex = Random.Range(0, currentSpawnPoints.Count);
            Instantiate(_spawnPoints[currentSpawnIndex].group, _spawnPoints[currentSpawnIndex].spawnPointTransform);
            currentSpawnPoints.RemoveAt(currentSpawnIndex);
        }
    }
}
