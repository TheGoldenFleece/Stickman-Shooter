using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public event EventHandler OnGameRun;
    public event EventHandler OnCutscene;
    public event EventHandler OnBossFight;
    public event EventHandler OnGameOver;
    public event EventHandler OnPassLevel;

    [SerializeField] private float cutsceneDuration = 2.0f;
    [SerializeField] private bool isBonusLevel;
    public bool IsBonusLevel => isBonusLevel;
    public enum State {
        Idle,
        RunStarted,
        Cutscene,
        BossFightStarted,
        GameOver,
        PassLevel,
    }
    public State state { get; set; }

    private void Awake() {
        if (Instance != null) {
            return;
        }
        Instance = this;

        state = State.Idle;
    }

    private void Update() {
        switch (state) {
            case State.Idle: {
                    break;
                }
            case State.RunStarted: {
                    OnGameRun?.Invoke(this, EventArgs.Empty);

                    state = State.Idle;
                    break;
                }

            case State.Cutscene: {

                    OnCutscene?.Invoke(this, EventArgs.Empty);
                    StartCoroutine(CutsceneCoroutine(cutsceneDuration));

                    state = State.Idle;

                    break;
            }
            case State.BossFightStarted: {
                    OnBossFight?.Invoke(this, EventArgs.Empty);

                    state = State.Idle;

                    break;
                }
            case State.GameOver: {
                    OnGameOver?.Invoke(this, EventArgs.Empty);

                    state = State.Idle;

                    break;
                }
            case State.PassLevel: {
                    OnPassLevel?.Invoke(this, EventArgs.Empty);

                    state = State.Idle;
                    break;
                }
        }
    }

    IEnumerator CutsceneCoroutine(float duration) {
        yield return new WaitForSeconds(duration);

        state = State.BossFightStarted;
        yield break;
    }
}
