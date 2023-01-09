using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShaiHuludSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _shaiHuludSpawnPointsRoot;
    
    [SerializeField] private GameObject _prefabShaiHulud;
    [SerializeField] private GameObject _prefabShaiHuludPit;
    
    [SerializeField] private float _respawnTimeMin;
    [SerializeField] private float _respawnTimeMax;

    [SerializeField] private float _stayTimeMin;
    [SerializeField] private float _stayTimeMax;

    [SerializeField] private float _pitTimeMin;
    [SerializeField] private float _pitTimeMax;

    [SerializeField] private float _inOutDurationSeconds;
    
    [SerializeField] private AudioSource _audioSourceShaiHuludWalking;

    private SpawnPoint[] _spawnPoints;
    
    private void Awake()
    {
        _spawnPoints = _shaiHuludSpawnPointsRoot.GetComponentsInChildren<SpawnPoint>();
    }

    private void Start()
    {
        StartCoroutine(SpawnLoopCoroutine());
    }

    private IEnumerator SpawnLoopCoroutine()
    {
        while (true)
        {
            var respawnTimer = Random.Range(_respawnTimeMin, _respawnTimeMax);

            _audioSourceShaiHuludWalking.Play();
            
            yield return new WaitForSeconds(respawnTimer);
            
            var spawnPointIndex = Random.Range(0, _spawnPoints.Length);
            var spawnPoint = _spawnPoints[spawnPointIndex];

            var pitPosition = new Vector3(spawnPoint.transform.position.x, -1.0f, spawnPoint.transform.position.z);
            
            var pit = Instantiate(_prefabShaiHuludPit, pitPosition, Quaternion.identity);
            var pitController = pit.GetComponent<ShaiHuludPitController>();
            
            yield return pit.transform.DOMoveY(0.0f, 0.3f).WaitForCompletion();
            
            var pitTime = Random.Range(_pitTimeMin, _pitTimeMax);

            yield return new WaitForSeconds(pitTime);

            _audioSourceShaiHuludWalking.Pause();

            var shaiHuludPosition = new Vector3(spawnPoint.transform.position.x, -4.0f, spawnPoint.transform.position.z);
            
            var activeShaiHulud = Instantiate(_prefabShaiHulud, shaiHuludPosition, Quaternion.identity);

            var shaiHuludController = activeShaiHulud.GetComponent<ShaiHuludController>();

            yield return shaiHuludController.ShowYourself(_inOutDurationSeconds);
            
            pitController.HideSand();
            
            var stayTime = Random.Range(_stayTimeMin, _stayTimeMax);

            yield return WaitShaiHuludOrDropIfEaten(stayTime, shaiHuludController);
            
            pitController.ShowSand();
            
            yield return shaiHuludController.HideYourself(_inOutDurationSeconds);
            
            Destroy(activeShaiHulud);

            yield return pit.transform.DOMoveY(-1.0f, 0.3f).WaitForCompletion();
                
            Destroy(pit);
        }
    }

    private IEnumerator WaitShaiHuludOrDropIfEaten(float stayTime, ShaiHuludController shaiHuludController)
    {
        while (true)
        {
            stayTime -= Time.deltaTime;

            if (stayTime <= 0)
            {
                break;
            }

            if (shaiHuludController.IsHumanEaten)
            {
                break;
            }

            yield return null;
        }
    }
}
