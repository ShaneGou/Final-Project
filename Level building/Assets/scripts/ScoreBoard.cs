using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    public static int score = 0;
    static int scoreGoal = 30;

    // void OnGUI()
    // {
    //     GUILayout.Box("score:" + score);
    // }

    private void Update()
    {
        if (score >= scoreGoal) 
        {
            score = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScene");
        }
    }

    // public static int GetScore()
    // {
    //     return score;
    // }

    // public static int GetScoreGoal()
    // {
    //     return scoreGoal;
    // }

}
