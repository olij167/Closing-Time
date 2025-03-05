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

    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

    public GameSettings gameSettings;

    public TMP_Dropdown levelSelectDropdown;
    public TMP_Dropdown itemNumberDropdown;

    public TMP_Dropdown gameLengthDropdown;

    public Toggle itemMultiplesToggle;

    public void ToggleStartPanel()
    {
        if (!setUpPanel.activeSelf)
        {
            if (SaveSystem.RememberStat(gameSettings) != null)
            {
                GameStats stats = SaveSystem.RememberStat(gameSettings);
                //statsPanel.SetActive(true);

                accuracyText.text = "Accuracy: " + stats.accuracyScore;
                budgetText.text = "Budget: " + stats.budgetScore;
                timeText.text = "Time Left: " + stats.timeTaken + "s / " + stats.fullTime + "s";
                scoreText.text = "Score: " + stats.shopperScore;

            }
            else
            {
                accuracyText.text = "Accuracy: N/A";
                budgetText.text = "Budget: N/A";
                timeText.text = "Time Left: N/A";
                scoreText.text = "Score: N/A";
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
