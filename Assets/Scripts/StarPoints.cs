using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarPoints : MonoBehaviour
{

    void Start()
    {
        transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0f), 1.0f, 0, 0.0f).SetLoops(-1);
    }

}