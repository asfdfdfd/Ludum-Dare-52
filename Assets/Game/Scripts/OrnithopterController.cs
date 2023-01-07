using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrnithopterController : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    [SerializeField] private float _xboundary = 9.5f;
    [SerializeField] private float _zboundary = 4.5f;

    [FormerlySerializedAs("_beamSpawnPoint")] [SerializeField] private GameObject _dropSpawnPoint;

    [SerializeField] private GameObject _beamPrefab;
    
    [SerializeField] private GameObject _humanPrefab;

    private ActiveBeamManager _activeBeamManager;

    private void Awake()
    {
        var systemGameObject = GameObject.FindWithTag("System");
        _activeBeamManager = systemGameObject.GetComponent<ActiveBeamManager>();
    }

    private void Update()
    {
        var directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        var newPosition = gameObject.transform.position + directionVector * _speed * Time.deltaTime;

        if (newPosition.x > _xboundary)
        {
            newPosition = new Vector3(_xboundary, newPosition.y, newPosition.z);
        }
        
        if (newPosition.x < -_xboundary)
        {
            newPosition = new Vector3(-_xboundary, newPosition.y, newPosition.z);
        }
        
        if (newPosition.z > _zboundary)
        {
            newPosition = new Vector3(newPosition.x, newPosition.y, _zboundary);
        }
        
        if (newPosition.z < -_zboundary)
        {
            newPosition = new Vector3(newPosition.x, newPosition.y, -_zboundary);
        }

        gameObject.transform.position = newPosition;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnBeam();
        } 
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnHuman();
        }
    }

    private void SpawnBeam()
    {
        var activeBeam = Instantiate(_beamPrefab, _dropSpawnPoint.transform.position, Quaternion.identity);

        _activeBeamManager.ActiveBeam = activeBeam;
    }

    private void SpawnHuman()
    {
        Instantiate(_humanPrefab, _dropSpawnPoint.transform.position, Quaternion.identity);
    }
}
