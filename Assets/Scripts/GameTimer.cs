using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEditor.Progress;

public class GameTimer : MonoBehaviour
{
    private ShoppingList shoppingList;
    public float timer;
    public bool timerActive;
    public bool gameOver;

    public string menuScene;

    public TextMeshProUGUI timerText;
    public GameObject endGameScreen;
    public TextMeshProUGUI listItemsStatText;
    public TextMeshProUGUI extraItemsStatText;
    public TextMeshProUGUI budgetStatText;
    public TextMeshProUGUI shopperScoreStatText;
    public TextMeshProUGUI leaveEarlyBonusText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        shoppingList = FindObjectOfType<ShoppingList>();
    }
    public void StartTimer()
    {
        timerActive = true;
    }

    public void Update()
    {
        timerText.text = timer.ToString("00:00");
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                gameOver = true;
                timerActive = false;
            }
        }

        if (gameOver)
        {
            SetStats();
            //display end game screen
            endGameScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Display end game stats

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

        listItemsStatText.text = shoppingList.itemsCollected + " / " + shoppingList.itemsRequired + "items collected from list";
    
        //Number of extra items collected
        int extraItemsCollected = 0;

        for (int i = 0; i < shoppingList.itemsInCart.Count; i++)
        {
            if (!shoppingList.itemsInCart[i].isOnList)
            {
                extraItemsCollected += shoppingList.itemsInCart[i].numberCollected;
            }
        }

        extraItemsStatText.text = extraItemsCollected + " items weren't on the list";

        //Budget Accuracy
        budgetStatText.text = "You spent $" + shoppingList.totalCost + ". The budget was $" + shoppingList.listBudget;

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
