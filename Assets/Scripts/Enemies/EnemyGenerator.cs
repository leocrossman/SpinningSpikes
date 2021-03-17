using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPatterns;
    GameObject currentPattern;
    int index;

    int timerStop;
    int timer;

    GameObject gem;

    void Awake()
    {
        timerStop = 300;
        timer = timerStop;
    }

    void FixedUpdate()
    {
        timer++;
        if (timer >= timerStop)
        {
            timer = 0;
            // enemyPatterns = GameObject.FindGameObjectsWithTag("enemyPattern"); // we made our patterns public and added in the inspector.
            index = Random.Range(0, enemyPatterns.Length);
            currentPattern = enemyPatterns[index];
            if (currentPattern != null)
            {
                // GameObject newEnemy = Instantiate(currentPattern, new Vector2(currentPattern.transform.position.x, currentPattern.transform.position.y + 7), currentPattern.transform.rotation);
                // Destroy(newEnemy, 10);
                StartCoroutine(EnemySpawner());
            }
        }
    }

    IEnumerator EnemySpawner()
    {
        GameObject newEnemyPattern = ObjectPool.SharedInstance.GetRandomPooledObject();
        if (newEnemyPattern != null)
        {
            newEnemyPattern.transform.position = new Vector2(currentPattern.transform.position.x, currentPattern.transform.position.y + 10);
            newEnemyPattern.transform.rotation = currentPattern.transform.rotation;
            newEnemyPattern.SetActive(true);
            ResetPatternPosition(newEnemyPattern);
            ActivateAllChildren(newEnemyPattern);
            yield return new WaitForSeconds(15);
            newEnemyPattern.SetActive(false);
        }
    }

    void ResetPatternPosition(GameObject obj)
    {
        obj.transform.position = new Vector3(0, 10, 0);
    }

    // Helper function mainly to reactivate powerups deactivated from object pooling
    void ActivateAllChildren(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
