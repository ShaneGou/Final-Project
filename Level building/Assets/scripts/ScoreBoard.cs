using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    public static int score = 0;
    static int scoreGoal = 30;

    private void Update()
    {
        if (score >= scoreGoal) 
        {
            score = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScene");
        }
    }
}
