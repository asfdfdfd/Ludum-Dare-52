using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        var x = Random.Range(0, 360);
        
        _rigidbody.MoveRotation(Quaternion.Euler(x, 0, 0));
    }

    public void DestroyWithShaiHuludTeeths()
    {
        Destroy(gameObject);
    }

    public void DestroyWithHeight()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            DestroyWithHeight();
        }
    }
}
