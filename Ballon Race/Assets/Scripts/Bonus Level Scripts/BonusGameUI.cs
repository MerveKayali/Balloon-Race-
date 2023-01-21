using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusGameUI : MonoBehaviour
{
    public static BonusGameUI instance;

    public GameObject  LeaderBoard;
    private Button nextLevel;
    public TextMeshProUGUI counterText,moneyText;

    public Image fill;

    public Sprite orange, gray;

    public TextMeshProUGUI currentLevelText, nextLevelText;
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        OpenLB();
    }

    IEnumerator StartGame()
    {
        counterText.text = 3.ToString();
        yield return new WaitForSeconds(1);
        counterText.color = Color.blue;
        counterText.text = 2.ToString();
        yield return new WaitForSeconds(1);
        counterText.color = Color.yellow;
        counterText.text = 1.ToString();
        yield return new WaitForSeconds(1);
        counterText.color = Color.green;
        counterText.text = "GO";
        BonusManager.instance.start = true;
        yield return new WaitForSeconds(0.5f);
        counterText.gameObject.SetActive(false);
    }

    public void OpenLB()//leader board
    {        
        if (GameObject.Find("BigBalloon")==null)
        {         
           
            LeaderBoard.SetActive(true);
            currentLevelText.text = PlayerPrefs.GetInt("Level", 1) - 1 + "";
            nextLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            moneyText.text = (BonusPlayerController.instance.knifeCounter*3).ToString()+" $";
            fill.sprite = orange;
        }                                 
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);//main menu index 0'da olacak;
    }

}
