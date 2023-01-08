using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private float _beamSpawnCooldown = 3.0f;

    [SerializeField] private float _humanSpawnCooldown = 3.0f;

    [SerializeField] private TextMeshProUGUI _textBeamCounter;
    
    [SerializeField] private TextMeshProUGUI _textHumanCounter;

    [SerializeField] private AudioSource _audioFlying;

    [SerializeField] private float audioFlyingMax = 0.3f;
    [SerializeField] private float audioFlyingMin = 0.1f;
    [SerializeField] private float _soundSpeed = 0.2f;

    [SerializeField] private AudioSource _audioSourceThrow;
    
    private ActiveBeamManager _activeBeamManager;

    private float _beamSpawnCooldownTimer;

    private float _humanSpawnCooldownTimer;

    private bool _isFlying = false;
    
    private void Awake()
    {
        var systemGameObject = GameObject.FindWithTag("System");
        _activeBeamManager = systemGameObject.GetComponent<ActiveBeamManager>();
    }

    private void Start()
    {
        _audioFlying.volume = audioFlyingMin;
    }

    private void UpdateBeamCooldownText()
    {
        _textBeamCounter.text = Mathf.CeilToInt(_beamSpawnCooldownTimer).ToString();
    }
    
    private void UpdateHumanCooldownText()
    {
        _textHumanCounter.text = Mathf.CeilToInt(_humanSpawnCooldownTimer).ToString();
    }

    private void Update()
    {
        if (_beamSpawnCooldownTimer > 0.0f)
        {
            _beamSpawnCooldownTimer -= Time.deltaTime;
            
            if (_beamSpawnCooldownTimer < 0)
            {
                _beamSpawnCooldownTimer = 0.0f;
            }
            
            UpdateBeamCooldownText();
        }
        
        if (_humanSpawnCooldownTimer > 0.0f)
        {
            _humanSpawnCooldownTimer -= Time.deltaTime;
            
            if (_humanSpawnCooldownTimer < 0)
            {
                _humanSpawnCooldownTimer = 0.0f;
            }
            
            UpdateHumanCooldownText();
        }        

        var directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        _isFlying = directionVector.sqrMagnitude > Mathf.Epsilon;
        
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
        
        TuneFlyingSound(Time.deltaTime);
    }

    private void TuneFlyingSound(float deltaTime)
    {
        var minusAddon = _isFlying ? 1 : -1;
        
        _audioFlying.volume += minusAddon * deltaTime * _soundSpeed;

        if (_audioFlying.volume > audioFlyingMax)
        {
            _audioFlying.volume = audioFlyingMax;
        }

        if (_audioFlying.volume < audioFlyingMin)
        {
            _audioFlying.volume = audioFlyingMin;
        }
    }
    
    private void SpawnBeam()
    {
        if (_beamSpawnCooldownTimer == 0.0f)
        {
            _audioSourceThrow.Play();
            
            var activeBeam = Instantiate(_beamPrefab, _dropSpawnPoint.transform.position, Quaternion.identity);

            _activeBeamManager.ActiveBeam = activeBeam;

            _beamSpawnCooldownTimer = _beamSpawnCooldown;

            UpdateBeamCooldownText();
        }
    }

    private void SpawnHuman()
    {
        if (_humanSpawnCooldownTimer == 0.0f)
        {
            _audioSourceThrow.Play();
            
            Instantiate(_humanPrefab, _dropSpawnPoint.transform.position, Quaternion.identity);
            
            _humanSpawnCooldownTimer = _humanSpawnCooldown;
            
            UpdateHumanCooldownText();
        }
    }
}
