using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private OrnithopterController _ornithopter;

    [SerializeField] private DialogSystemController _dialogSystemController;

    private bool isFire1PressedInDialog = false;
    private bool isFire2PressedInDialog = false;
    
    private void Update()
    {
        if (!GameState.IsTutorialDisplayed)
        {
            bool isFire1PressedInDialogNew = Input.GetAxis("Fire2") > 0.0f;
            bool isFire1Pressed = !isFire1PressedInDialog && isFire1PressedInDialogNew;
            isFire1PressedInDialog = isFire1PressedInDialogNew;
                
            if (isFire1Pressed || Input.GetKeyDown(KeyCode.E))
            {
                _dialogSystemController.SkipIntro();
            }

            bool isFire2PressedInDialogNew = Input.GetAxis("Fire1") > 0.0f;
            bool isFire2Pressed = !isFire2PressedInDialog && isFire2PressedInDialogNew;
            isFire2PressedInDialog = isFire2PressedInDialogNew;
            
            if (isFire2Pressed || Input.GetKeyDown(KeyCode.Space))
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
