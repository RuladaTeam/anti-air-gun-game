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
        public float ofsetOfSpawn;
        public bool canSpawn;
    }

    [Space(20)]
    [SerializeField] private float ofsetBetweenSpawns;
    [SerializeField] private float _ofsetOfSpawnPoints;
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private float startOfsetOfSpawnPoints;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < _spawnPoints.Count; i++)
        {
            startOfsetOfSpawnPoints = _ofsetOfSpawnPoints;
            _spawnPoints[i] = new SpawnPoint { spawnPointTransform = _spawnPoints[i].spawnPointTransform, ofsetOfSpawn = _ofsetOfSpawnPoints, canSpawn = true };
        }
    }

    private void Update()
    {
        if (_ofsetOfSpawnPoints > 0)
        {
            _ofsetOfSpawnPoints -= Time.deltaTime;
        }
        else
        {
            _ofsetOfSpawnPoints = startOfsetOfSpawnPoints;

        }
    }

    private void Spawn()
    {
        int countOfSpawn = Random.Range(1, _spawnPoints.Count);
        List<SpawnPoint> currentSpawnPoints = _spawnPoints;
        for(int i = 0; i < countOfSpawn; i++)
        {
            int currentSpawnIndex = Random.Range(0, _spawnPoints.Count);
            if (_spawnPoints[currentSpawnIndex].canSpawn)
            {
                Instantiate(_spawnPoints[currentSpawnIndex].group);
            }
            else
            {
                i--;
            }
            currentSpawnPoints.RemoveAt(currentSpawnIndex);
            StartCoroutine(CanSpawnCoroutine(_spawnPoints[currentSpawnIndex]));
        }
    }

    private IEnumerator CanSpawnCoroutine(SpawnPoint spawnPoint)
    {
        spawnPoint.canSpawn = false;
        yield return new WaitForSeconds(spawnPoint.ofsetOfSpawn);
        spawnPoint.canSpawn = true;
    }


}
