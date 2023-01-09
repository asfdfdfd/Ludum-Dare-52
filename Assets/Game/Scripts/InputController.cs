using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private OrnithopterController _ornithopter;

    [SerializeField] private DialogSystemController _dialogSystemController;
    
    private void Update()
    {
        if (!GameState.IsTutorialDisplayed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _dialogSystemController.SkipIntro();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dialogSystemController.NextDialog();
            }
        }
        else
        {
            var directionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

            _ornithopter.Move(directionVector);

            if (Input.GetAxis("Fire1") > 0.0f)
            {
                _ornithopter.SpawnBeam();
            }
            else if (Input.GetAxis("Fire2") > 0.0f)
            {
                _ornithopter.SpawnHuman();
            }
        }
    }
}
