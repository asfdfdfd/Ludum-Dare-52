using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShaiHuludController : MonoBehaviour
{
    private bool _isHumanEaten;

    [SerializeField] private GameObject _prefabSpice;

    [SerializeField] private ShaiHuludBeamTrigger _beamTrigger;
    
    private PointsController _pointsController;

    private Rigidbody _rigidbody;

    private ActiveBeamManager _activeBeamManager;
    
    private void Awake()
    {
        var gameObjectSystem = GameObject.FindWithTag("System");
        _pointsController = gameObjectSystem.GetComponent<PointsController>();
        _activeBeamManager = gameObjectSystem.GetComponent<ActiveBeamManager>();
        
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        var humanController = other.gameObject.GetComponent<HumanController>();
        
        if (humanController != null)
        {
            if (!_isHumanEaten)
            {
                _isHumanEaten = true;
                
                _pointsController.AddPointsForHuman();

                humanController.DestroyWithShaiHuludTeeths();
            }
            else
            {
                humanController.DestroyWithHeight();
            }
        }
    }
    
    public IEnumerator ShowYourself()
    {
        yield return _rigidbody.DOMoveY(1.0f, 1.0f).WaitForCompletion();
    }

    public IEnumerator HideYourself()
    {
        if (_beamTrigger.Beam == _activeBeamManager.ActiveBeam)
        {
            _activeBeamManager.ActiveBeam = null;
        }
        
        yield return _rigidbody.DOMoveY(-2.0f, 1.0f).WaitForCompletion();

        if (_isHumanEaten)
        {
            var spicePosition = new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
            
            Instantiate(_prefabSpice, spicePosition, Quaternion.identity);
        }
    }
}
