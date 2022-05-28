using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager: MonoBehaviour
{
    [SerializeField]
    float scorePerSecond = 10;
    [Serializable]
    struct Speedup {
        
        [SerializeField] public float time;
        [SerializeField] public float relativeSpeedup;
    }
    [SerializeField]
    Speedup[] speedups;
    [SerializeField] float speedup;
    public float GetSpeedup() { return speedup;  }
    [SerializeField] int currentSpeedup;
    [SerializeField] float gameRuntime;

    public static GameManager Instance;
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        speedup = 1.0f;
        UpdateGameState(GameState.TitleScreen);
    }
    public GameState State { get; set; }

    public static event Action<GameState> OnGameStateChanged;
    public void UpdateGameState(GameState newState) {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        State = newState;
        switch (newState) {
            case GameState.TitleScreen:
                if (player != null) {
                    player.GetComponent<PlayerController>().enabled = false;
                    player.GetComponent<PlayerHealth>().enabled = false;
                }
                break;
            case GameState.Playing:
                // reset health
                if(player!=null) {
                    score = 0;
                    gameRuntime = 0f;
                    currentSpeedup = 0;
                    speedup = 1.0f;
                    player.GetComponent<PlayerHealth>().ResetHealth();
                    player.GetComponent<PlayerHealth>().enabled = true;
                    player.GetComponent<PlayerController>().enabled = true;
                }
                break;
            case GameState.Dead:
                if (player != null) {
                    player.GetComponent<PlayerController>().enabled = false;
                    player.GetComponent<PlayerHealth>().enabled = false;
                }
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    double score;
    public int Score { get { return (int)score; } }
    double highscore;
    public int HighScore { get { return (int)highscore; } }
    public void AddScore(int delta) {
        score += delta;
    }
    public bool IsGameRunning() {
        return State == GameState.Playing;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (IsGameRunning()) {
            gameRuntime += Time.deltaTime;
            score += Time.deltaTime * scorePerSecond * speedup;
            if (score > highscore)
                highscore = score;
            CheckSpeedup();
        }
    }

    private void CheckSpeedup() {
        if (currentSpeedup >= speedups.Length)
            return;
        if(gameRuntime>speedups[currentSpeedup].time) {
            speedup = speedups[currentSpeedup].relativeSpeedup;
            currentSpeedup++;
        }
    }
}

public enum GameState {
    TitleScreen,
    Playing,
    Dead,
}