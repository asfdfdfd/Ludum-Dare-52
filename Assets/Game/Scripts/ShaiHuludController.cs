using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaiHuludController : MonoBehaviour
{
    private bool isHumanEaten = false;
    
    private void OnCollisionEnter(Collision other)
    {
        var humanController = other.gameObject.GetComponent<HumanController>();
        
        if (humanController != null)
        {
            if (isHumanEaten != null)
            {
                isHumanEaten = true;

                humanController.DestroyWithShaiHuludTeeths();
            }
            else
            {
                humanController.DestroyWithHeight();
            }
        }
    }
}
