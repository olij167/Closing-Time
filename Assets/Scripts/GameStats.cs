using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStats
{
    //Save stats for each game on end screen

    //Game settings (level, time, item num)
    //End game stats (accuracy, budget, shopper score)

    public string level;

    public float accuracyScore;
    public float budgetScore;
    public float shopperScore;
    public float fullTime;
    public float timeTaken;

    public GameStats (GameSettings gameSettings, GameTimer gameTimer)
    {
        level = gameSettings.selectedLevel;

        accuracyScore = Mathf.Round(gameTimer.requiredItemAccuracy * 100);
        budgetScore = Mathf.Round(gameTimer.budget * 100);
        shopperScore = Mathf.Round(gameTimer.shopperScore);
        timeTaken = Mathf.Round(gameTimer.timer);
        fullTime = gameTimer.fullTimer;
    }
}
