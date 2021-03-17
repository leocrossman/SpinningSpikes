using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gem : MonoBehaviour
{

    void Start()
    {
        transform.DOPunchScale(new Vector3(0.01f, 0.01f, 0f), 1.0f, 0, 0.0f).SetLoops(-1);
    }


}

