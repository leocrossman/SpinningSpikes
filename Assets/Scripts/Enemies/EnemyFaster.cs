using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using DG.Tweening;

public class EnemyFaster : MonoBehaviour
{
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

    float moveSpeed = 0.3001f;

    void FixedUpdate()
    {
        // move enemies down screen
        // transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y - 0.05f), 0.1f);
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y - moveSpeed), 0.1f);
    }
}
