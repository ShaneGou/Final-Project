using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int currentGold;
    public Text goldText;

    private int activeScene;
    private int nextScene;

    void Start() 
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = activeScene + 1;
    }
    void Update()
    {
        if (currentGold >= 10) {
            currentGold = 0;
            
            SceneManager.LoadScene(nextScene);
        }
    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold;
        ScoreBoard.score = ScoreBoard.score + 1;
    }
}
