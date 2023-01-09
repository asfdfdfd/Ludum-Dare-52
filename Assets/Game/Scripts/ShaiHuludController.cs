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

    private Rigidbody _rigidbody;

    private ActiveBeamManager _activeBeamManager;

    private float _startY;
    
    private void Awake()
    {
        var gameObjectSystem = GameObject.FindWithTag("System");
        gameObjectSystem.GetComponent<PointsController>();
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

                humanController.DestroyWithShaiHuludTeeths();
            }
            else
            {
                humanController.DestroyWithHeight();
            }
        }
    }
    
    public IEnumerator ShowYourself(float duration)
    {
        _audioSourceShaiHuludEnter.Play();
        
        yield return _rigidbody.DOMoveY(0.0f, duration).WaitForCompletion();

        _audioSourceShaiHuludEnter.DOFade(0.0f, 0.1f);
    }

    public IEnumerator HideYourself(float duration)
    {
        _audioSourceShaiHuludExit.Play();
        
        if (_beamTrigger.Beam == _activeBeamManager.ActiveBeam)
        {
            _activeBeamManager.ActiveBeam = null;
        }
        
        yield return _rigidbody.DOMoveY(_startY, duration).WaitForCompletion();
        
        if (_isHumanEaten)
        {
            var spicePosition = new Vector3(gameObject.transform.position.x, 0.01f, gameObject.transform.position.z);
            
            Instantiate(_prefabSpice, spicePosition, Quaternion.identity);
        }

        yield return _audioSourceShaiHuludExit.DOFade(0.0f, 0.1f).WaitForCompletion();
    }
}
