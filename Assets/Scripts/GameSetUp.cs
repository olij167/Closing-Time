using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameTimer gameTimer;
    public ShoppingList shoppingList;

    private void Start()
    {
        SetUpGame();

        gameTimer.StartTimer();
    }

    public void SetUpGame()
    {
        shoppingList.numOfItems = gameSettings.numberOfItems;
        shoppingList.multipleOfSameItem = gameSettings.multiplesOfItems;

        shoppingList.GenerateListItems();

        gameTimer.timer = gameSettings.amountOfTime;
    }
}
