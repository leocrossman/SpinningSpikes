using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // get reference to game manager
    GameManager gameManager;

    Rigidbody2D rb;

    private Vector3 pos;

    Transform currentEnemyPattern;
    // GameObject currentEnemyPattern;
    Transform currentEnemy;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        TouchMove();
        // AddScore();
    }

    public void TouchMove()
    {
        if (Input.touchCount > 0)
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5));

            transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                gameManager.GameOver();
                break;
            case "wall":
                gameManager.GameOver();
                break;
            case "powerPoints":
                currentEnemyPattern = col.transform.parent.gameObject.transform;
                foreach (Transform enemy in currentEnemyPattern)
                {
                    enemy.localScale = new Vector3(0.2f,0.2f,0);
                }
                gameManager.Score += 100;
                Destroy(col.gameObject);
                break;
            case "powerGem":
                // TODO: CHANGE BELOW LINE AND MAKE THIS SHIT ADD 1 TO THE USER'S GEM COUNT YERRRRRR
                // gameManager.Score += 100;
                gameManager.Gems += 1;
                // Destroy(col.transform.parent.gameObject);  // not using pooling
                col.transform.parent.gameObject.SetActive(false); // using pooling
                break;            
        }
    }
}
