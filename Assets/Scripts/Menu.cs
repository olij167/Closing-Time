using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject setUpPanel;

    public GameSettings gameSettings;

    public TMP_Dropdown levelSelectDropdown;
    public TMP_Dropdown itemNumberDropdown;

    public TMP_Dropdown gameLengthDropdown;

    public Toggle itemMultiplesToggle;

    public void ToggleStartPanel()
    {
        if (!setUpPanel.activeSelf)
        {
            setUpPanel.SetActive(true);
            SelectLevel();
            SetGameLength();
            SetListLength();
            ToggleItemMultiples();
        }
        else
            setUpPanel.SetActive(false);
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
        gameSettings.amountOfTime = int.Parse(gameLengthDropdown.options[gameLengthDropdown.value].text);
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

    public void ToggleItemMultiples()
    {
        gameSettings.multiplesOfItems = itemMultiplesToggle.isOn;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
