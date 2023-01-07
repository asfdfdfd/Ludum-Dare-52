using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    
    private ActiveBeamManager _activeBeamManager;

    private void Awake()
    {
        var systemGameObject = GameObject.FindWithTag("System");
        _activeBeamManager = systemGameObject.GetComponent<ActiveBeamManager>();
    }

    private void Update()
    {
        var activeBeam = _activeBeamManager.ActiveBeam;
        if (activeBeam != null)
        {
            var distance = _speed * Time.deltaTime;
            var positionTarget = new Vector3(activeBeam.transform.position.x, gameObject.transform.position.y, activeBeam.transform.position.z);
            var positionNew = Vector3.MoveTowards(gameObject.transform.position, positionTarget, distance);
            gameObject.transform.position = positionNew;
        }
    }
}
