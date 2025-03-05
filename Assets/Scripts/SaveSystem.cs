using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{

    //Add a way to remember level & score, and only show the top one

    //save last data and best data in seperate files for each level
    //check best data on save
    //overwrite if new data is better than last data

    public static void SaveStats(GameSettings gameSettings, GameTimer gameTimer)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        //int logFileNo = 1;
        //string fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);

        //while (File.Exists(fileName))
        //{
        //    logFileNo++;
        //    fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);
        //}

        //Debug.Log("Saved File: " + fileName);

        string path = Application.persistentDataPath + "/" + gameSettings.selectedLevel + ".shopperscore";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameStats stats = new GameStats(gameSettings, gameTimer);
        Debug.Log("Stats saved at path: " + path);

        formatter.Serialize(stream, stats);
        stream.Close();


    }

    public static GameStats RememberStat(GameSettings gameSettings)
    {
        //int logFileNo = 1;
        //string fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);

        //while (File.Exists(fileName))
        //{
        //    logFileNo++;
        //    fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);
        //}

        //Debug.Log("Found File: " + fileName);

        string path = Application.persistentDataPath + "/" + gameSettings.selectedLevel + ".shopperscore";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameStats stats = formatter.Deserialize(stream) as GameStats;
            Debug.Log("Stats found at path: " + path);


            stream.Close();


            return stats;

        }
        else
        {
            Debug.Log("No previous stats to show");
            return null;
        }
    }


}
