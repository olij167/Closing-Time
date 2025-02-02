using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameTimer gameTimer;
    public ShoppingList shoppingList;
    public StockManager stockManager;

    private void Start()
    {
        SetUpGame();

    }

    public void SetUpGame()
    {
        shoppingList.numOfItems = gameSettings.numberOfItems;
        shoppingList.multipleOfSameItem = gameSettings.multiplesOfItems;

        stockManager.SetStock();
        shoppingList.GenerateListItems(stockManager.itemsInStore);

        if (!gameSettings.unlimitedTime)
        {
            gameTimer.timer = gameSettings.amountOfTime;
            gameTimer.StartTimer();
        }
        else
        {
            gameTimer.timerActive = false;
            gameTimer.timerText.text = "Unlimited Time";
        }
    }
}
