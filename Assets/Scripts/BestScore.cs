﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SquareConstants;

public class BestScore : MonoBehaviour {

    private GameSession gameSession;
    private Text bestScoreText;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
        bestScoreText = GetComponent<Text>();
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        if (Social.localUser.authenticated) {
            if (SceneManager.GetActiveScene().name == "Level1") {
                gameSession.GetUserScore((score) => { gameSession.bestScore = score; }, GPGSIds.leaderboard_top_players);
            } else if (SceneManager.GetActiveScene().name == "Level2") {
                gameSession.GetUserScore((score) => { gameSession.bestScore = score; }, GPGSIds.leaderboard_top_impossible_players);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        ShowBestScore();
    }

    private void ShowBestScore() {
        bestScoreText.text = gameSession.GetBestScore().ToString();
        if (gameSession.GetBestScore() > gameSession.GetScore()) {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
