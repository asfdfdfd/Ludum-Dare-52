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
            Destroy(_activeBeam);
            
            _activeBeam = value;
        }
    }
}
