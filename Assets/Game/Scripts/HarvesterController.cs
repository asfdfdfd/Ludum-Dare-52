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

    [SerializeField] private GameObject _smokeHarvest;
    [SerializeField] private GameObject _vfxMove;
    
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
            
            var distanceToTarget = Vector3.Distance(positionTarget, positionNew);

            _isMoving = distanceToTarget > 0.001f;

            _vfxMove.SetActive(_isMoving);
            
            if (_isMoving)
            {
                _rigidbody.MovePosition(positionNew);

                Vector3 targetDirection = (positionNew - _rigidbody.position).normalized;
                var targetRotation = Quaternion.LookRotation(targetDirection);
                _rigidbody.MoveRotation(targetRotation);
                
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

            var activeBeam = _activeBeamManager.ActiveBeam;
            if (activeBeam != null)
            {
                activeBeam.GetComponent<BeamController>().DiePlease();
                _activeBeamManager.ActiveBeam = null;
            }
            
            onHarvesterDestroyed?.Invoke();
            
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
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
                _smokeHarvest.SetActive(true);
                
                if (!_audioSourceWorking.isPlaying)
                {
                    _audioSourceWorking.Play();
                }
            }
            else
            {
                _smokeHarvest.SetActive(false);
                
                _audioSourceWorking.Stop();
            }

            int points = (int) (_spicePerSecond * Time.deltaTime * 100);
            _pointsController.AddPointsForSpice(points);
        }
    }
}
