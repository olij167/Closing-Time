using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSetUp : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameTimer gameTimer;
    public ShoppingList shoppingList;
    public StockManager stockManager;

    public TextMeshProUGUI countdownTimer;
    public float fullTimer;
    public float timer = 5f;

    bool gameStarted;
    private void Start()
    {
        fullTimer = timer;
        SetUpGame();
        
    }

    public void SetUpGame()
    {
        shoppingList.numOfItems = gameSettings.numberOfItems;
        //shoppingList.multipleOfSameItem = gameSettings.multiplesOfItems;

        stockManager.SetStock();
        shoppingList.GenerateListItems(stockManager.itemsInStore);

        if (!gameSettings.unlimitedTime)
        {
            gameTimer.timer = gameSettings.amountOfTime;
            FindObjectOfType<FirstPersonMovement>().enabled = false;
            FindObjectOfType<InteractionRaycast>().enabled = false;
            FindObjectOfType<FirstPersonCam>().enabled = false;
            //StartCoroutine(CountdownTimer());
        }
        else
        {
            gameTimer.timerActive = false;
            gameTimer.timerText.text = "Unlimited Time";
            countdownTimer.enabled = false;

        }
    }


    public void Update()
    {
        if (!gameStarted)
        {
            if (timer >= 1)
            {
                gameTimer.timerText.text = gameTimer.fullTimer.ToString("00");
                timer -= Time.deltaTime;
                countdownTimer.text = timer.ToString("0");
                countdownTimer.color = Color.Lerp(gameTimer.timerOriginal, gameTimer.timerDire, (fullTimer - timer) / fullTimer);

                shoppingList.shoppingListContainer.transform.localScale = Vector3.one * 2;
            }
            else if (timer < 2 && timer > 0)
            {
                timer -= Time.deltaTime;
                countdownTimer.color = Color.Lerp(gameTimer.timerOriginal, gameTimer.timerDire, (fullTimer - timer) / fullTimer);

                countdownTimer.text = "GO!";

                shoppingList.shoppingListContainer.transform.localScale = Vector3.Lerp(Vector3.one, shoppingList.shoppingListContainer.transform.localScale, timer);

            }
            else if (timer <= 0)
            {
                gameTimer.StartTimer();
                countdownTimer.enabled = false;
                shoppingList.shoppingListContainer.transform.localScale = Vector3.one;
                gameStarted = true;

            }
        }
    }

  
}
