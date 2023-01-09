using UnityEngine;

public class DialogSystemStarter : MonoBehaviour
{
    [SerializeField] private GameObject _dialogSystem;
    
    private void Start()
    {
        GameState.IsTutorialDisplayed = true;
        
        _dialogSystem.SetActive(!GameState.IsTutorialDisplayed);
    }
}
