using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HarvesterController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    [SerializeField] private float _spicePerSecond = 10.0f;

    [SerializeField] private AudioSource _audioSourceMoving;
    [SerializeField] private AudioSource _audioSourceWorking;
    
    private Rigidbody _rigidbody;
    
    private ActiveBeamManager _activeBeamManager;
    private PointsController _pointsController;
    
    private bool _isHarvesterDestroyed = false;

    public UnityEvent onHarvesterDestroyed;

    private bool _isMoving;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        var systemGameObject = GameObject.FindWithTag("System");
        _activeBeamManager = systemGameObject.GetComponent<ActiveBeamManager>();
        _pointsController = systemGameObject.GetComponent<PointsController>();
    }

    private void FixedUpdate()
    {
        if (_isHarvesterDestroyed)
        {
            return;
        }
        
        var activeBeam = _activeBeamManager.ActiveBeam;
        if (activeBeam != null)
        {
            var distance = _speed * Time.fixedDeltaTime;
            var positionTarget = new Vector3(activeBeam.transform.position.x, _rigidbody.position.y, activeBeam.transform.position.z);
            var positionNew = Vector3.MoveTowards(_rigidbody.position, positionTarget, distance);

            _rigidbody.MovePosition(positionNew);

            var distanceToTarget = Vector3.Distance(positionTarget, positionNew);

            _isMoving = distanceToTarget > 0.001f; 
            
            if (_isMoving)
            {
                if (!_audioSourceMoving.isPlaying)
                {
                    _audioSourceMoving.Play();
                }
            }
            else
            {
                _audioSourceMoving.Stop();
            }
        }
        else
        {
            _audioSourceMoving.Stop();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var shaiHulud = other.gameObject.GetComponent<ShaiHuludController>();
        
        if (shaiHulud != null)
        {
            _isHarvesterDestroyed = true;
            
            onHarvesterDestroyed?.Invoke();
            
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (_isMoving)
        {
            return;
        }
        
        var spiceController = other.gameObject.GetComponent<SpiceController>();

        if (spiceController != null)
        {
            float spiceRemaining = spiceController.Damage(_spicePerSecond * Time.deltaTime);

            if (spiceRemaining > 0.0f)
            {
                if (!_audioSourceWorking.isPlaying)
                {
                    _audioSourceWorking.Play();
                }
            }
            else
            {
                _audioSourceWorking.Stop();
            }

            int points = (int) (_spicePerSecond * Time.deltaTime * 100);
            _pointsController.AddPointsForSpice(points);
        }
    }
}
