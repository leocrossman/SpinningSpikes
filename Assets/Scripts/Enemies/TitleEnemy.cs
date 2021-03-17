using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleEnemy : MonoBehaviour
{
    // public float rotateSpeed = 1000f;
    // public int rotateSpeed = 1;

    void FixedUpdate()
    {
        // transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * rotateSpeed);
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * 3);
    }
}
