using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamGameObjectStorage : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    public GameObject BeamGameObject => _gameObject;
}
