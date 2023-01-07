using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HarvesterManager : MonoBehaviour
{
    [SerializeField] private GameObject _harvesterPrefab;

    [SerializeField] private int _harvestersCount = 3;

    private GameObject _activeHarvester;
    private HarvesterController _activeHarvesterController;
    
    public int HarvestersCount => _harvestersCount;
    
    private void Awake()
    {
        SpawnHarvester();
    }

    private void SpawnHarvester()
    {
        _activeHarvester = Instantiate(_harvesterPrefab, gameObject.transform.position, Quaternion.identity);
        _activeHarvesterController = _activeHarvester.GetComponent<HarvesterController>();
        
        _activeHarvesterController.onHarvesterDestroyed.AddListener(() =>
        {
            OnHarversterDestroyed();
        });
    }
    
    private void OnHarversterDestroyed()
    {
        _harvestersCount--;

        if (_harvestersCount == -1)
        {
            throw new NotImplementedException("YEAH BOI");
        }
        else
        {
            SpawnHarvester();
        }
    }
}
