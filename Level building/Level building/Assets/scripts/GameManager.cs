using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int currentGold;
    public Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGold >= 10) {
            currentGold = 0;
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Gold: " + currentGold + "!";
        ScoreBoard.score = ScoreBoard.score + 1;
    }
}
