using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public TMP_InputField playerName;

    public TextMeshProUGUI currentLevelText, nextLevelText;
    public static MenuManager instance;
    void Start()
    {
        currentLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
        nextLevelText.text = PlayerPrefs.GetInt("Level", 1) + 1+"";


        if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
            PlayerPrefs.SetInt("FirstTime", 1);
        else
            playerName.text = PlayerPrefs.GetString("PlayerName");
    }

    public void StartGame()
    {
        if (playerName.text == "")
            PlayerPrefs.SetString("PlayerName", "Player");
        else
            PlayerPrefs.SetString("PlayerName", playerName.text);

        SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1));
    }
}
