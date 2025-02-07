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
    public float timer;
    public bool timerActive;
    public bool gameOver;

    public string menuScene;

    public TextMeshProUGUI timerText;
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
    public TextMeshProUGUI leaveEarlyBonusText;

    private ToggleCursorLock toggleCursor;

    private bool endGameSet = false;
    private void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        shoppingList = FindObjectOfType<ShoppingList>();
        itemScanner = FindObjectOfType<ItemScanner>();

        endGameScreen.SetActive(false);
    }
    public void StartTimer()
    {
        timerActive = true;
    }

    public void Update()
    {
        if (timerActive)
        {
            timerText.text = timer.ToString("00:00");

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                gameOver = true;
                timerActive = false;
            }
        }

        if (gameOver)
        {
            FindObjectOfType<FirstPersonMovement>().enabled = false;
            FindObjectOfType<InteractionRaycast>().enabled = false;

            SetStats();
            //display end game screen
            endGameScreen.SetActive(true);
            if (!endGameSet)
            {
                itemScanner.SpawnItemsEndGame();
                endGameSet = true;
            
            }
            toggleCursor.UnlockCursor();
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

        accuracyScoreText.text = "Accuracy: " + accuracy * 100;
        accuracyInfoText.text += "\n" + extraItemsCollected + " items weren't on the list";



        budgetScoreText.text = "Budget: " + budget * 100;
        //Budget Accuracy
        budgetInfoText.text = "You spent $" + shoppingList.totalCost + ". \n The budget was $" + shoppingList.listBudget;

         

        //Debug.Log("accuracy = " + accuracy + "\n" + "budget = " + budget);
        shopperScore = accuracy * budget * 100; // << score formula.. did I enter it wrong?
        shopperScoreStatText.text = "Shopper Score: " + shopperScore + " / 100";

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
