using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] levelsButtons;

    void Start()
    {
        foreach (Button button in levelsButtons)
        {
            button.interactable = false;
        }
        levelsButtons[0].interactable = true;
        int levelsUnlocked = 0;
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 1);
        }
        else
        {
       levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked");
       for (int i = 0; i < levelsUnlocked; i++)
       {
           
           if (i < levelsButtons.Length)
           {
               levelsButtons[i].interactable = true;
           }
       }
       
       Debug.Log("Total Levels Unlocked are " + levelsUnlocked);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
