using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRevolve : MonoBehaviour
{
    public int revolveAngle = 45;

    void Update()
    {
        // make enemies revolve around parent
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), revolveAngle * Time.deltaTime);
    }
}
