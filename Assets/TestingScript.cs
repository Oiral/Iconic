using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {

    public int testScore = 100;

    private void Start()
    {
        SaveSystem.ClearScores();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ScoreData data = SaveSystem.LoadScores();
            if (data.highScores != null)
            {
                data.highScores.Add(testScore);
                SaveSystem.SaveScore(data);
            }
            else
            {
                Debug.Log("Creating new save");
                SaveSystem.SaveScore(testScore);
            }
            DebugList(SaveSystem.LoadScores().highScores);

        }
	}

    private void DebugList(List<int> list)
    {
        string debugString = "";
        for (int i = 0; i < list.Count; i++)
        {
            debugString += list[i].ToString() + " | ";
        }
        Debug.Log(debugString);
    }
}
