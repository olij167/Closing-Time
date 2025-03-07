using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Menu : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject setUpPanel;
    //public GameObject statsPanel;

    public TextMeshProUGUI lastAccuracyText;
    public TextMeshProUGUI lastBudgetText;
    public TextMeshProUGUI lastTimeText;
    public TextMeshProUGUI lastScoreText;
     
    public TextMeshProUGUI bestAccuracyText;
    public TextMeshProUGUI bestBudgetText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI bestScoreText;

    public GameSettings gameSettings;

    public TMP_Dropdown levelSelectDropdown;
    public TMP_Dropdown itemNumberDropdown;

    public TMP_Dropdown gameLengthDropdown;

    public Toggle itemMultiplesToggle;

    public void ToggleStartPanel()
    {
        if (!setUpPanel.activeSelf)
        {
            if (SaveSystem.RememberLastStat(gameSettings) != null)
            {
                GameStats lastStats = SaveSystem.RememberLastStat(gameSettings);
                //statsPanel.SetActive(true);

                lastAccuracyText.text = "Accuracy: " + lastStats.accuracyScore;
                lastBudgetText.text = "Budget: " + lastStats.budgetScore;
                lastTimeText.text = "Time Left: " + lastStats.timeTaken + "s / " + lastStats.fullTime + "s";
                lastScoreText.text = "Score: " + lastStats.shopperScore;

            }
            else
            {
                lastAccuracyText.text = "Accuracy: N/A";
                lastBudgetText.text = "Budget: N/A";
                lastTimeText.text = "Time Left: N/A";
                lastScoreText.text = "Score: N/A";
                //statsPanel.SetActive(true);
            }
            
            if (SaveSystem.RememberBestStat(gameSettings) != null)
            {
                GameStats bestStats = SaveSystem.RememberBestStat(gameSettings);
                //statsPanel.SetActive(true);

                bestAccuracyText.text = "Accuracy: " + bestStats.accuracyScore;
                bestBudgetText.text = "Budget: " + bestStats.budgetScore;
                bestTimeText.text = "Time Left: " + bestStats.timeTaken + "s / " + bestStats.fullTime + "s";
                bestScoreText.text = "Score: " + bestStats.shopperScore;

            }
            else
            {
                bestAccuracyText.text = "Accuracy: N/A";
                bestBudgetText.text = "Budget: N/A";
                bestTimeText.text = "Time Left: N/A";
                bestScoreText.text = "Score: N/A";
                //statsPanel.SetActive(true);
            }


            setUpPanel.SetActive(true);
            SelectLevel();
            SetGameLength();
            SetListLength();
            //ToggleItemMultiples();
            titlePanel.SetActive(false);
        }
        else
        {
            setUpPanel.SetActive(false);
            titlePanel.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSettings.selectedLevel);
    }

    public void SelectLevel()
    {
        gameSettings.selectedLevel = levelSelectDropdown.options[levelSelectDropdown.value].text;

    }
    public void SetGameLength()
    {
        bool canParse = int.TryParse(gameLengthDropdown.options[gameLengthDropdown.value].text, out int x);

        if (canParse)
        {
            gameSettings.amountOfTime = int.Parse(gameLengthDropdown.options[gameLengthDropdown.value].text);
            gameSettings.unlimitedTime = false;
        }
        else
        {
            gameSettings.unlimitedTime = true;
        }
        //switch (gameLengthDropdown.value)
        //{
        //    case 0:
        //        gameSettings.amountOfTime = 60f;
        //        break;
        //    case 1:
        //        gameSettings.amountOfTime = 45f;
        //        break;
        //    case 2:
        //        gameSettings.amountOfTime = 30f;
        //        break;
        //    case 3:
        //        gameSettings.amountOfTime = 15f;
        //        break;
        //}
    }
    
    public void SetListLength()
    {
        gameSettings.numberOfItems = itemNumberDropdown.value + 1;
    }

    //public void ToggleItemMultiples()
    //{
    //    gameSettings.multiplesOfItems = itemMultiplesToggle.isOn;
    //}
    public void Quit()
    {
        Application.Quit();
    }
}
