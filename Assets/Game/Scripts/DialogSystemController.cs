using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystemController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogsRoot;

    private List<GameObject> _dialogs = new List<GameObject>();

    private int _currentDialog = -1;
    
    private void Start()
    {
        var dialogsCount = _dialogsRoot.transform.childCount;

        for (int dialogIndex = 0; dialogIndex < dialogsCount; dialogIndex++)
        {
            var dialog = _dialogsRoot.transform.GetChild(dialogIndex);
            
            _dialogs.Add(dialog.gameObject);
            
            dialog.gameObject.SetActive(false);
        }
        
        NextDialog();
    }

    public void SkipIntro()
    {
        GameState.IsTutorialDisplayed = true;
        Destroy(gameObject);   
    }

    public void NextDialog()
    {
        var previousDialog = _currentDialog;
        
        _currentDialog++;

        if (_currentDialog >=_dialogs.Count)
        {
            SkipIntro();
            return;
        }

        if (previousDialog >= 0)
        {
            _dialogs[previousDialog].SetActive(false);
        }
        
        _dialogs[_currentDialog].SetActive(true);
    }
}
