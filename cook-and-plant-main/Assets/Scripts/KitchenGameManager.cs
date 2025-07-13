using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    
    public static KitchenGameManager Instance { get; private set; }

    [SerializeField] private float  gamePlayingTimerMax = 50f;

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State 
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float  gamePlayingTimer;
    private bool isGamePaused = false;
    private bool isPlayerLose = false;

    [SerializeField] public int maxOrganicTrash = 10;
    [SerializeField] public int maxAnorganicTrash = 10;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start() 
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        int trashCount = OrganicTrashCounter.Instance.GetCurrentOrganicTrash() + AnorganicTrashCounter.Instance.GetCurrentAnorganicTrash();

        switch(state) 
        {
            case State.WaitingToStart:
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) 
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) 
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                else if(trashCount == maxOrganicTrash + maxAnorganicTrash) 
                {
                    isPlayerLose = true;
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                break;
        }
    }

    public bool IsPlayerLose()
    {
        return isPlayerLose;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1- (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
        
    }
    
    public int GetMaxAnorganicTrash()
    {
        return maxAnorganicTrash;
    }

    public int GetMaxOrganicTrash()
    {
        return maxOrganicTrash;
    }
}
