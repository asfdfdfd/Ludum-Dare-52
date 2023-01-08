using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ShaiHuludController : MonoBehaviour
{
    private bool _isHumanEaten;

    [SerializeField] private GameObject _prefabSpice;

    [SerializeField] private ShaiHuludBeamTrigger _beamTrigger;

    [SerializeField] private AudioSource _audioSourceEatHuman;
    [SerializeField] private AudioSource _audioSourceShaiHuludEnter;
    [SerializeField] private AudioSource _audioSourceShaiHuludExit;
    
    private PointsController _pointsController;

    private Rigidbody _rigidbody;

    private ActiveBeamManager _activeBeamManager;

    private float _startY;
    
    private void Awake()
    {
        var gameObjectSystem = GameObject.FindWithTag("System");
        _pointsController = gameObjectSystem.GetComponent<PointsController>();
        _activeBeamManager = gameObjectSystem.GetComponent<ActiveBeamManager>();
        
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _startY = gameObject.transform.position.y;
    }

    private void OnCollisionEnter(Collision other)
    {
        var humanController = other.gameObject.GetComponent<HumanController>();
        
        if (humanController != null)
        {
            if (!_isHumanEaten)
            {
                _audioSourceEatHuman.Play();
                
                _isHumanEaten = true;
                
                _pointsController.AddPointsForHuman();

                humanController.DestroyWithShaiHuludTeeths();
            }
            else
            {
                humanController.DestroyWithHeight();
            }
        }
    }
    
    public IEnumerator ShowYourself()
    {
        _audioSourceShaiHuludEnter.Play();
        
        yield return _rigidbody.DOMoveY(0.0f, 2.0f).WaitForCompletion();
    }

    public IEnumerator HideYourself()
    {
        _audioSourceShaiHuludExit.Play();
        
        if (_beamTrigger.Beam == _activeBeamManager.ActiveBeam)
        {
            _activeBeamManager.ActiveBeam = null;
        }
        
        yield return _rigidbody.DOMoveY(_startY, 2.0f).WaitForCompletion();

        if (_isHumanEaten)
        {
            var spicePosition = new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
            
            Instantiate(_prefabSpice, spicePosition, Quaternion.identity);
        }
    }
}
