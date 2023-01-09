using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    [SerializeField] private GameObject vfxDeath;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        var x = Random.Range(0, 360);
        
        _rigidbody.MoveRotation(Quaternion.Euler(x, 0, 0));
    }

    public void DestroyWithShaiHuludTeeths()
    {
        Destroy(_rigidbody);
        Destroy(_collider);
        
        vfxDeath.SetActive(true);
    }

    public void DestroyWithHeight()
    {
        Destroy(_rigidbody);
        Destroy(_collider);

        StartCoroutine(AnimateDeathFromHeight());
    }

    private IEnumerator AnimateDeathFromHeight()
    {
        vfxDeath.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        yield return gameObject.transform.DOMoveY(-1.0f, 3.0f).WaitForCompletion();
        
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
