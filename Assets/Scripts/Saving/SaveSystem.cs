using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    static string ScorePath = "/score.savedata";

	public static void SaveScore(int score)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ScorePath;
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreData data = new ScoreData();
        data.highScores.Add(score);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveScore(ScoreData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ScorePath;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData LoadScores()
    {
        string path = Application.persistentDataPath + ScorePath;
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ScoreData data = (ScoreData)formatter.Deserialize(stream);

            stream.Close();
             
            return data;
            
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void ClearScores() {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + ScorePath;
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreData data = new ScoreData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void DebugList(List<int> list)
    {
        string debugString = "";
        for (int i = 0; i < list.Count; i++)
        {
            debugString += list[i].ToString() + " | ";
        }
        Debug.Log(debugString);
    }
}
