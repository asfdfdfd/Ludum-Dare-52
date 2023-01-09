using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPoints;
    
    private void Start()
    {
        GameState.Points = 0;
    }

    private void UpdateTextPoints()
    {
        _textPoints.text = GameState.Points.ToString();
    }
    
    private void AddPoints(int points)
    {
        GameState.Points += points;

        UpdateTextPoints();
    }

    public void AddPointsForSpice(int amountOfSpice)
    {
        AddPoints(amountOfSpice);
    }
}
