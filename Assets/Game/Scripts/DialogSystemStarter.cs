using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystemStarter : MonoBehaviour
{
    private void Start()
    {
        if (GameState.IsTutorialDisplayed)
        {
            // Destroy(gameObject);
            // return;
        }
    }
}
