using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenePlateController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(AnimatePlateCoroutine());
    }

    private IEnumerator AnimatePlateCoroutine()
    {
        yield return null;
    }
}
