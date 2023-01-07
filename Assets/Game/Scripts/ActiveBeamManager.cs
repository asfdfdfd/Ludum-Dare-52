using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBeamManager : MonoBehaviour
{
    private GameObject _activeBeam;

    public GameObject ActiveBeam
    {
        get
        {
            return _activeBeam;
        }
        
        set
        {
            if (_activeBeam != null)
            {
                _activeBeam.GetComponent<BeamController>().DiePlease();
            }

            _activeBeam = value;
        }
    }
}
