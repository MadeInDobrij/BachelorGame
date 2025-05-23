using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    
    public void Select(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
        Destroy(GameObject.Find("Audio Source"));
    }

    public void Transition()
    {
        SceneManager.LoadScene(0);
    }
}
