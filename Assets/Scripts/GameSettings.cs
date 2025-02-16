using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]

public class GameSettings : ScriptableObject
{
    public string selectedLevel;
    public int numberOfItems;
    //public bool multiplesOfItems;
    public float amountOfTime;
    public bool unlimitedTime;
}
