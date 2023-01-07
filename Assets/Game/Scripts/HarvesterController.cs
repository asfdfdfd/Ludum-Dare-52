using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HarvesterController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    [SerializeField] private float _spicePerSecond = 10.0f;
    
    private Rigidbody _rigidbody;
    
    private ActiveBeamManager _activeBeamManager;

    private bool _isHarvesterDestroyed = false;

    public UnityEvent onHarvesterDestroyed;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        var systemGameObject = GameObject.FindWithTag("System");
        _activeBeamManager = systemGameObject.GetComponent<ActiveBeamManager>();
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
        var spiceController = other.gameObject.GetComponent<SpiceController>();

        if (spiceController != null)
        {
            spiceController.Damage(_spicePerSecond * Time.deltaTime);
        }
    }
}
