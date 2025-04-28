
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawn : MonoBehaviour
{
    [SerializeField, Min(1)] private int _maxSpawns;
    [SerializeField] private float ofsetBetweenSpawns;
    [SerializeField] private List<GameObject> planeTrajectories = new List<GameObject>();
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    private float startOfsetBetweenSpawns;

    private void OnDisable()
    {
        MovingObjectTraectory[] movingObjects = FindObjectsByType<MovingObjectTraectory>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );
        foreach (var component in movingObjects)
        {
            Destroy(component.gameObject);
        }
    }

    void Start()
    {
        Spawn();
        startOfsetBetweenSpawns = ofsetBetweenSpawns;
        if (_maxSpawns > _spawnPoints.Count)
        {
            _maxSpawns = _spawnPoints.Count;
        }
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
        List<Transform> currentSpawnPoints = new List<Transform>(_spawnPoints);
        for(int i = 0; i < countOfSpawn; i++)
        {
            int currentPlane = Random.Range(0, planeTrajectories.Count);
            int currentSpawnIndex = Random.Range(0, currentSpawnPoints.Count);
            Instantiate(planeTrajectories[currentPlane], currentSpawnPoints[currentSpawnIndex]).SetActive(true);
            currentSpawnPoints.RemoveAt(currentSpawnIndex);
            if(currentSpawnPoints.Count < 2)
            {
                break;
            }
        }
    }
}
