using System.Collections;
using DG.Tweening;
using UnityEngine;

public class HarvesterDeadController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(HideHarvester());
    }

    private IEnumerator HideHarvester()
    {
        yield return new WaitForSeconds(3.0f);

        yield return gameObject.transform.DOMoveY(-1, 1.0f).WaitForCompletion();
        
        Destroy(gameObject);
    }
}
