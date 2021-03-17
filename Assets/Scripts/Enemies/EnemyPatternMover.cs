using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternMover : MonoBehaviour
{
    float moveSpeed = 0.3f;

    void FixedUpdate()
    {
        // move enemies down screen
        // transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y - 0.05f), 0.1f);
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y - moveSpeed), 0.1f);
    }

    // void OnEnable()
    // {
    //     // when pattern is enabled for object pooling, reset its position to y = 10
    //     transform.position = new Vector3(0, 10, 0);
    //     // transform.position = new Vector3(0, 10, 0);
    // }
}
