using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{ 
    public enum GameState { 
        COUNTDOWN_PHASE,
        SHOOT_PHASE,
        END_ROUND_PHASE,
        RESET_PHASE,
        END_DUAL_PHASE
    };

    [SerializeField] private ReadyLightBehavior ReadyLight;
    [SerializeField] private PlayerBehavior player1;
    [SerializeField] private PlayerBehavior player2;
    [SerializeField] private ThumbleweedBehavior thumbleweed;

    private GameState gameState = GameState.RESET_PHASE;
    private bool isGameOver = false;
    private Timer endRoundTimer;
    private const int WinNeeded = 3;

    // Start is called before the first frame update
    private void Awake()
    {
        endRoundTimer = GetComponent<Timer>();
    }

    private void Start()
    {
        LockPlayerControls();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isGameOver)
        {
            switch (gameState)
            {
                case GameState.COUNTDOWN_PHASE:
                    CountdownPhaseUpdate();
                    break;
                case GameState.SHOOT_PHASE:
                    ShootPhaseUpdate();
                    break;
                case GameState.END_ROUND_PHASE:
                    EndPhaseUpdate();
                    break;
                case GameState.RESET_PHASE:
                    ResetPhaseUpdate();
                    break;
                case GameState.END_DUAL_PHASE:
                    EndDualPhaseUpdate();
                    break;
                default:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if(gameState == GameState.COUNTDOWN_PHASE)
        {
            thumbleweed.Move();
        }
    }

    private void CountdownPhaseUpdate()
    {
        ReadyLight.Count();

        if (!ReadyLight.randomTimer.isOver)
        {
            if (player1.hasShoot) player1.IsDead();
            if (player2.hasShoot) player2.IsDead();
            if (player1.isDead || player2.isDead) gameState = GameState.END_ROUND_PHASE;
        }
        else if (ReadyLight.randomTimer.isOver)
        {
            gameState = GameState.SHOOT_PHASE;
        }
    }

    private void ShootPhaseUpdate()
    {
        if (player1.isDead || player2.isDead) gameState = GameState.END_ROUND_PHASE;
        if (player1.hasShoot) player2.IsDead();
        if (player2.hasShoot) player1.IsDead();
    }

    private void EndPhaseUpdate()
    {
        LockPlayerControls();
        ReadyLight.Wait();

       if(player1.isDead && !player2.isDead)
        {
            player2.WinRound();
            Debug.Log("Player 2 Win! Points : " + player2.RoundsWin);
        }
       else if(!player1.isDead && player2.isDead)
        {
            player1.WinRound();
            Debug.Log("Player 1 Win! Points : " + player1.RoundsWin);
        }
       else if(player1.isDead && player2.isDead)
        {
            Debug.Log("Draw! No one win!");
        }

       if (player1.RoundsWin >= WinNeeded || player2.RoundsWin >= WinNeeded)
        {
            gameState  = GameState.END_DUAL_PHASE;
        }
        else gameState = GameState.RESET_PHASE;
    }

    private void ResetPhaseUpdate()
    {
        endRoundTimer.UpdateTimer();
   
        if(endRoundTimer.isOver)
        {
            player1.Reset();
            player2.Reset();
            thumbleweed.Reset();
            ReadyLight.Reset();
            endRoundTimer.Reset();
            gameState = GameState.COUNTDOWN_PHASE;
        }
    }

    private void EndDualPhaseUpdate()
    {
        if (player1.RoundsWin == WinNeeded) player1.HasWon();
        else if (player2.RoundsWin == WinNeeded) player2.HasWon();
        player1.ResetScore();
        player2.ResetScore();
        gameState = GameState.RESET_PHASE;
    }

    private void LockPlayerControls()
    {
        player1.LockControl();
        player2.LockControl();
    }
}