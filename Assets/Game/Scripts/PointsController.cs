using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPoints;

    private int _points;

    private void UpdateTextPoints()
    {
        _textPoints.text = _points.ToString();
    }
    
    private void AddPoints(int points)
    {
        _points += points;

        UpdateTextPoints();
    }

    public void AddPointsForSpice(int amountOfSpice)
    {
        AddPoints(amountOfSpice);
    }

    public void AddPointsForHuman()
    {
        AddPoints(100);
    }
}
