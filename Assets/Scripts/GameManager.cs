using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject _player;

    public TextMeshProUGUI menuHighscoreText;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pressToPlayText;

    public TextMeshProUGUI gemText;

    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighscoreText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelGameOver;

    public GameObject enemyGeneratorPrefab;
    private GameObject _enemyGenerator;

    public static GameManager Instance { get; private set; }

    public enum State { MENU, INIT, PLAY, GAMEOVER }
    State _state;
    bool _isSwitchingState;

    private float _score;
    private float _scoreMultiplier = 2f;
    public float Score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreText.text = "" + _score.ToString("F0");
        }
    }

    private int _gems;
    public int Gems
    {
        get {return _gems;}
        set
        {
            _gems = value;
            PlayerPrefs.SetInt("gems",PlayerPrefs.GetInt("gems")+1);
            gemText.text = "" + _gems.ToString();
            // gemText.text = "" + PlayerPrefs.GetInt("gems").ToString("F0");
        }
    }

    public void PlayClicked()
    {
        SwitchState(State.INIT);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
        // PlayerPrefs.DeleteKey("gems");
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    public IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }

    public void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                menuHighscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetFloat("highscore").ToString("F0");
                gemText.text = "" + PlayerPrefs.GetInt("gems").ToString();
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                panelPlay.SetActive(true);
                Score = 0;
                SwitchState(State.PLAY);
                break;
            case State.PLAY:
                pressToPlayText.text = "PRESS AND HOLD TO PLAY";
                break;
            case State.GAMEOVER:
                if (Score > PlayerPrefs.GetFloat("highscore"))
                {
                    PlayerPrefs.SetFloat("highscore", Score);
                }
                panelGameOver.SetActive(true);
                gameOverScoreText.text = "SCORE: " + Score.ToString("F0");
                gameOverHighscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetFloat("highscore").ToString("F0");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            pressToPlayText.text = "";
                            _player = Instantiate(playerPrefab);
                            _enemyGenerator = Instantiate(enemyGeneratorPrefab,new Vector2(0,8),Instance.transform.rotation);

                        }
                        else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) // if player alive, add score
                        {
                            AddScore();
                        }
                        // if dead, switch to gameover state here...
                        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) // add logic for player collision with enemy
                        {
                            GameOver();
                        }
                    }
                // otherwise, spawn enemies...
                break;
            case State.GAMEOVER:
                // SwitchState(State.MENU);
                break;
        }
    }

    public void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.GAMEOVER:
                // panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                SwitchState(State.MENU);
                break;
        }
    }

    public void GameOver()
    {
        Destroy(_player);
        Destroy(_enemyGenerator);
        // RemoveEnemies(); // dont use with pooling!
        RemovePowerups();
        RemovePatterns();
        panelPlay.SetActive(false);
        SwitchState(State.GAMEOVER);
    }

    void RemoveEnemies()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void RemovePowerups()
    {
        GameObject[] powerups;
        powerups = GameObject.FindGameObjectsWithTag("power");
        foreach (GameObject power in powerups)
        {
            // without pooling
            // Destroy(power);

            // with pooling
            power.SetActive(false);
        }
    }

    void RemovePatterns()
    {
        GameObject[] patterns;
        patterns = GameObject.FindGameObjectsWithTag("enemyPattern");
        foreach (GameObject pattern in patterns)
        {
            // without pooling
            // Destroy(pattern);

            // with pooling
            pattern.SetActive(false);
        }
    }

    void AddScore()
    {
        Score += Time.deltaTime * _scoreMultiplier;
    }

    void AddGem()
    {

    }
}
