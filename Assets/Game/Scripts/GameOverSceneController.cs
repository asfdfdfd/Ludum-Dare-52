using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPoints;
    
    private void Start()
    {
        _textPoints.text = GameState.Points.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game/Scenes/GameplayScene");
        }
    }
}
