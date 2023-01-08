using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaiHuludBeamTrigger : MonoBehaviour
{
    private GameObject _beam;

    public GameObject Beam => _beam;
    
    private void OnTriggerEnter(Collider other)
    {
        var beamGameObjectStorage = other.GetComponent<BeamGameObjectStorage>();

        if (beamGameObjectStorage != null)
        {
            _beam = beamGameObjectStorage.BeamGameObject;
        }
    }
}
