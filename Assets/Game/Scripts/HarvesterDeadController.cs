using System.Collections;
using DG.Tweening;
using UnityEngine;

public class HarvesterDeadController : MonoBehaviour
{
    [SerializeField] private GameObject _vfxSmoke;
    
    private void Start()
    {
        StartCoroutine(HideHarvester());
    }

    private IEnumerator HideHarvester()
    {
        yield return new WaitForSeconds(1.0f);
        
        _vfxSmoke.SetActive(true);
        
        yield return new WaitForSeconds(2.0f);

        _vfxSmoke.GetComponent<ParticleSystem>().Stop();
        
        yield return gameObject.transform.DOMoveY(-1, 1.0f).WaitForCompletion();
        
        Destroy(gameObject);
    }
}
