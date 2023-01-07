using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaiHuludController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        var humanController = other.gameObject.GetComponent<HumanController>();
        
        if (humanController != null)
        {
            Destroy(other.gameObject);    
        }
    }
}
