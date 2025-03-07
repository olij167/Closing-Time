using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Toolbelt_OJ;

public class GameTimer : MonoBehaviour
{
    private ShoppingList shoppingList;
    private ItemScanner itemScanner;
    private GameSetUp setUp;
    public float fullTimer;
    public float timer;
    public bool timerActive;
    public bool gameOver;

    public string menuScene;

    public TextMeshProUGUI timerText;
    public Color timerOriginal;
    public Color timerDire;
    public GameObject endGameScreen;
    public GameObject endGameStatsParent;

    public float accuracy;
    public TextMeshProUGUI accuracyScoreText;
    public TextMeshProUGUI accuracyInfoText;

    public float budget;
    public TextMeshProUGUI budgetScoreText;
    public TextMeshProUGUI budgetInfoText;

    public float shopperScore = 0f;
    public TextMeshProUGUI shopperScoreStatText;

    public float timeRemaining;
    public TextMeshProUGUI timerRemainingText;

    private ToggleCursorLock toggleCursor;

    public bool endGameSet;

    public TextMeshProUGUI bestScoreIndicator;
    private void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        shoppingList = FindObjectOfType<ShoppingList>();
        itemScanner = FindObjectOfType<ItemScanner>();

        setUp = FindObjectOfType<GameSetUp>();
        endGameScreen.SetActive(false);
        bestScoreIndicator.gameObject.SetActive(false);

        fullTimer = timer;
    }
    public void StartTimer()
    {
        timerText.enabled = true;

        timerActive = true;
        FindObjectOfType<FirstPersonMovement>().enabled = true;
            FindObjectOfType<FirstPersonCam>().enabled = true;
        FindObjectOfType<InteractionRaycast>().enabled = true;
    }

    public void Update()
    {
        if (timerActive)
        {
            timerText.text = timer.ToString("00");

            timer -= Time.deltaTime;

            timerText.color = Color.Lerp( timerOriginal, timerDire, (fullTimer - timer) / fullTimer);
            

            if (timer <= 0f)
            {
                gameOver = true;
                timerActive = false;
            }
        }

        if (gameOver)
        {

           if (!endGameScreen.activeSelf) EndGameScreen();

            //Display end game stats

            //if (itemScanner.showStats)
            //{
            //    endGameStatsParent.SetActive(true);
            //}
            //else
            //{
            //    endGameStatsParent.SetActive(false);
            //}

        }
    }

    public void EndGameScreen()
    {
        if (!endGameScreen.activeSelf)
        {
            FindObjectOfType<FirstPersonMovement>().enabled = false;
            FindObjectOfType<InteractionRaycast>().enabled = false;
            FindObjectOfType<FirstPersonCam>().enabled = false;

            SetStats();
            //display end game screen
            endGameScreen.SetActive(true);

            SaveSystem.SaveStats(setUp.gameSettings, this);

            GameStats bestStats = SaveSystem.RememberBestStat(setUp.gameSettings);

            if (bestStats != null)
            {
                bestScoreIndicator.gameObject.SetActive(true);

                if (bestStats.shopperScore == shopperScore)
                {
                    bestScoreIndicator.text = "New Highscore!";
                }
                else
                    bestScoreIndicator.text = "Best Score: " + bestStats.shopperScore;
            }

            itemScanner.SpawnItemsEndGame();

            toggleCursor.UnlockCursor();

            endGameSet = true;

        }
        else return;
    }

    public void SetStats()
    {
        // number of listed items collected
        //int listItemsCollected = 0;
        //int listItemsRequired = 0;

        //float listBudget = 0f;

        //for (int i = 0; i < shoppingList.shoppingListItems.Count; i++)
        //{
        //    listItemsRequired += shoppingList.shoppingListItems[i].numberRequired;
        //    listBudget += shoppingList.shoppingListItems[i].item.itemValue * shoppingList.shoppingListItems[i].numberRequired;

        //    listItemsCollected += shoppingList.shoppingListItems[i].numberCollected;
        //}

       
    
        //Number of extra items collected
        int extraItemsCollected = 0;

        if (shoppingList.listItemsCollected > shoppingList.listItemsRequired)
        {
            extraItemsCollected += shoppingList.listItemsCollected - shoppingList.listItemsRequired;
            //Debug.Log("extra " + (shoppingList.listItemsCollected - shoppingList.listItemsRequired).ToString());
        }

        accuracyInfoText.text = (shoppingList.listItemsCollected - extraItemsCollected) + " / " + shoppingList.listItemsRequired + "items collected from list";


        for (int i = 0; i < shoppingList.itemsInCart.Count; i++)
        {
            if (!shoppingList.itemsInCart[i].isOnList)
            {
                extraItemsCollected += shoppingList.itemsInCart[i].numberCollected;
            }
        }


        accuracy = (float)shoppingList.listItemsCollected / (float)shoppingList.listItemsRequired;
        budget = shoppingList.listBudget / shoppingList.totalCost;

        accuracyScoreText.text = "Accuracy: " + Mathf.Round(accuracy * 100);
        accuracyInfoText.text += "\n" + extraItemsCollected + " items weren't on the list";



        budgetScoreText.text = "Budget: " + Mathf.Round(budget * 100);
        //Budget Accuracy
        budgetInfoText.text = "You spent $" + shoppingList.totalCost + ". \n The budget was $" + shoppingList.listBudget;

        //timeRemaining = (timer / fullTimer) * 100;

        timerRemainingText.text = "Time Left: " + timer.ToString("00") + " / " + fullTimer + " secs";

        //Debug.Log("accuracy = " + accuracy + "\n" + "budget = " + budget);
        shopperScore = Mathf.Round( accuracy * budget * 100); // << score formula.. did I enter it wrong?
        shopperScoreStatText.text = "Shopper Score: \n" + shopperScore + " / 100";

        //Shopper Score
        // Compare other stats to get their final score

    }

    public void LeaveStore()
    {
        timerActive = false;
        gameOver = true;
        // apply a bonus for finishing early
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }    
}
