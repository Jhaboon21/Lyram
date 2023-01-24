using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void NextLevelButton(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
