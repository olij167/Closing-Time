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

        string lastPath = Application.persistentDataPath + "/" + gameSettings.selectedLevel + ".lastshopperscore";

        FileStream stream = new FileStream(lastPath, FileMode.Create);

        GameStats stats = new GameStats(gameSettings, gameTimer);
        Debug.Log("Last stats saved at path: " + lastPath);

        formatter.Serialize(stream, stats);
        stream.Close();

        //Check if there is a best stat to show
        string bestPath = Application.persistentDataPath + "/" + gameSettings.selectedLevel + ".bestshopperscore";
        if (File.Exists(bestPath))
        {
            BinaryFormatter bestFormatter = new BinaryFormatter();
            FileStream bestStream = new FileStream(bestPath, FileMode.Open);
            GameStats bestStats = bestFormatter.Deserialize(bestStream) as GameStats;
            Debug.Log("Best stats found at path: " + bestPath);

            bestStream.Close();

            //Check whether the new stats are better than the previous best stats
            if (stats.shopperScore > bestStats.shopperScore)
            {
                bestStream = new FileStream(bestPath, FileMode.Create);
                Debug.Log("Best stats saved at path: " + bestPath);

                formatter.Serialize(bestStream, stats); // save new stats under best stream
                bestStream.Close();
            }

        }
        else
        {
            Debug.Log("No best stats to show");

            BinaryFormatter bestFormatter = new BinaryFormatter();

            FileStream bestStream = new FileStream(bestPath, FileMode.Create);
            Debug.Log("Best stats saved at path: " + bestPath);

            bestFormatter.Serialize(bestStream, stats); // save new stats under best stream
            bestStream.Close();

        }
    }

    public static GameStats RememberLastStat(GameSettings gameSettings)
    {
        //int logFileNo = 1;
        //string fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);

        //while (File.Exists(fileName))
        //{
        //    logFileNo++;
        //    fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);
        //}

        //Debug.Log("Found File: " + fileName);

        string lastPath = Application.persistentDataPath + "/" + gameSettings.selectedLevel + ".lastshopperscore";

        if (File.Exists(lastPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(lastPath, FileMode.Open);
            GameStats stats = formatter.Deserialize(stream) as GameStats;
            Debug.Log("Last stats found at path: " + lastPath);


            stream.Close();


            return stats;

        }
        else
        {
            Debug.Log("No previous stats to show");
            return null;
        }
    }

    public static GameStats RememberBestStat(GameSettings gameSettings)
    {
        //int logFileNo = 1;
        //string fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);

        //while (File.Exists(fileName))
        //{
        //    logFileNo++;
        //    fileName = String.Format(gameSettings.selectedLevel + "_{0}.shopperscore", logFileNo);
        //}

        //Debug.Log("Found File: " + fileName);

        string bestPath = Application.persistentDataPath + "/" + gameSettings.selectedLevel + ".bestshopperscore";

        if (File.Exists(bestPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(bestPath, FileMode.Open);
            GameStats stats = formatter.Deserialize(stream) as GameStats;
            Debug.Log("Best stats found at path: " + bestPath);


            stream.Close();


            return stats;

        }
        else
        {
            Debug.Log("No best stats to show");
            return null;
        }
    }


}
