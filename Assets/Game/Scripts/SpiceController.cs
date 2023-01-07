using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceController : MonoBehaviour
{
    private float _health = 100.0f;

    public void Damage(float value)
    {
        _health -= value;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
