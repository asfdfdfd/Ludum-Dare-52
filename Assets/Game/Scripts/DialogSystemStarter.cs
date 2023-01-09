using UnityEngine;

public class DialogSystemStarter : MonoBehaviour
{
    [SerializeField] private GameObject _dialogSystem;
    
    private void Start()
    {
        _dialogSystem.SetActive(!GameState.IsTutorialDisplayed);
    }
}
