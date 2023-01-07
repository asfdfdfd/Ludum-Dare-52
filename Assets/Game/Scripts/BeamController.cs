using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceExplosion;
    [SerializeField] private AudioSource _audioSourceIdle;

    private bool _shouldDie;
    
    public void DiePlease()
    {
        _audioSourceIdle.Stop();
        _audioSourceExplosion.Play();

        _shouldDie = true;
    }

    private void Update()
    {
        if (_shouldDie)
        {
            if (!_audioSourceExplosion.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
