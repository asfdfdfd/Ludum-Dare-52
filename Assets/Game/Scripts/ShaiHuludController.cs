using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaiHuludController : MonoBehaviour
{
    private bool _isHumanEaten;

    [SerializeField] private GameObject _prefabSpice;
    
    private void OnCollisionEnter(Collision other)
    {
        var humanController = other.gameObject.GetComponent<HumanController>();
        
        if (humanController != null)
        {
            if (!_isHumanEaten)
            {
                _isHumanEaten = true;

                humanController.DestroyWithShaiHuludTeeths();

                Instantiate(_prefabSpice, gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                humanController.DestroyWithHeight();
            }
        }
    }
}
