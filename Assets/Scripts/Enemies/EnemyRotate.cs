using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * - ENEMY IS ALWAYS ROTATING SO USE THIS SCRIPT FOR HIGH LEVEL ENEMY LOGIC I.E. LOCALPOSITION
public class EnemyRotate : MonoBehaviour
{
    public int rotateSpeed = 5;

    Vector3 originalPosition;

    // first enable
    void Awake()
    {
        originalPosition = transform.localPosition;
    }

    // subsequent enables via object pooling
    void OnEnable()
    {
        transform.localPosition = originalPosition;
    }

    void Update()
    {
        // make enemies spin (in-place)
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * rotateSpeed);
    }
}
