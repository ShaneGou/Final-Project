using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] int levelNumber = 0;

    public void OpenScene() 
    {
        SceneManager.LoadScene(levelNumber, LoadSceneMode.Single);
    }
}
