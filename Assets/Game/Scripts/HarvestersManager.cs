using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HarvestersManager : MonoBehaviour
{
    [SerializeField] private GameObject _harvesterPrefab;

    [SerializeField] private int _harvestersCount = 3;

    [SerializeField] private TextMeshProUGUI _textHarversters;
    
    private GameObject _activeHarvester;
    private HarvesterController _activeHarvesterController;
    
    public int HarvestersCount => _harvestersCount;
    
    private void Awake()
    {
        SpawnHarvester();
        UpdateHarverstersText();
    }

    private void UpdateHarverstersText()
    {
        _textHarversters.text = _harvestersCount.ToString();
    }
    
    private void SpawnHarvester()
    {
        _activeHarvester = Instantiate(_harvesterPrefab, gameObject.transform.position, Quaternion.Euler(0.0f, -27.6f, 0.0f));
        _activeHarvesterController = _activeHarvester.GetComponent<HarvesterController>();
        
        _activeHarvesterController.onHarvesterDestroyed.AddListener(() =>
        {
            OnHarversterDestroyed();
        });
    }
    
    private void OnHarversterDestroyed()
    {
        _harvestersCount--;

        UpdateHarverstersText();

        if (_harvestersCount == -1)
        {
            SceneManager.LoadScene("Game/Scenes/GameOverScene");
        }
        else
        {
            SpawnHarvester();
        }
    }
}
