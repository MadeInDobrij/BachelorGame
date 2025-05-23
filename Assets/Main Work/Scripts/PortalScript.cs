using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    public int currentLevel;

    private void Start()
    {
        currentLevel =SceneManager.GetActiveScene().buildIndex-1;
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Portal Collided with " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entering Next Levels");
            SceneManager.LoadScene(1);
            if (PlayerPrefs.HasKey("LevelsUnlocked"))
            {
                
                int current = PlayerPrefs.GetInt("LevelsUnlocked");
                current += 1;
                if(current < currentLevel+2)
                PlayerPrefs.SetInt("LevelsUnlocked", current);
            }

            

        }
    }
}
