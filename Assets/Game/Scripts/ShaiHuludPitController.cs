using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaiHuludPitController : MonoBehaviour
{
    [SerializeField] private GameObject _vfxSand;

    public void ShowSand()
    {
        _vfxSand.SetActive(true);
    }

    public void HideSand()
    {
        _vfxSand.SetActive(true);
    }
}
