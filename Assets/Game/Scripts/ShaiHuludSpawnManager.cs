using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShaiHuludSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _shaiHuludSpawnPointsRoot;
    
    [SerializeField] private GameObject _prefabShaiHulud;

    [SerializeField] private float _respawnTime;

    private float _respawnTimer;

    private SpawnPoint[] _spawnPoints;

    private GameObject _activeShaiHulud;
    private void Awake()
    {
        RearmRespawnTimer();
        
        _spawnPoints = _shaiHuludSpawnPointsRoot.GetComponentsInChildren<SpawnPoint>();
    }

    private void RearmRespawnTimer()
    {
        _respawnTimer = _respawnTime;
    }
    
    private void Update()
    {
        _respawnTimer -= Time.deltaTime;
        
        if (_respawnTimer <= 0.0f)
        {
            SpawnShaiHulud();
            
            RearmRespawnTimer();
        }
    }

    private void SpawnShaiHulud()
    {
        Destroy(_activeShaiHulud);
        
        var spawnPointIndex = Random.Range(0, _spawnPoints.Length);
        var spawnPoint = _spawnPoints[spawnPointIndex];
        
        _activeShaiHulud = Instantiate(_prefabShaiHulud, spawnPoint.transform.position, Quaternion.identity);
    }
}
