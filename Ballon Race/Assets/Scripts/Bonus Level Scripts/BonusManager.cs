using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusManager : MonoBehaviour
{
    public bool finish, failed, start;
    public static BonusManager instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FinishGame()
    {
        if(finish)
        {
            if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("Level"))
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        }
           
    }
}
