using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    public GameObject InGame, LeaderBoard;
    private Button nextLevel;
    public TextMeshProUGUI counterText;

    public Image fill;

    public Sprite orange, gray;


    public TextMeshProUGUI currentLevelText, nextLevelText;
    void Awake()
    {
        instance = this;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {       
        
        if (!PlayerController.instance.player.activeInHierarchy)//player yok olmuþsa yani fail ise GameManager.failed yaptýðým zaman methoda girmedi
        {           
            if (LeaderBoard.activeInHierarchy)
            {               
                Restart();
                GameManager.instance.failed = false;
               
            }
        }
               
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
        GameManager.instance.start = true;
        yield return new WaitForSeconds(0.5f);
        counterText.gameObject.SetActive(false);
    }

    private void Restart()//next butonunu fail olursak again butonuna dönüþtürüp oyunun baþýna sarýyoruz
    {
        //nextLevel = GameObject.Find("/GameUI/LeaderBoardPanel/Next").GetComponent<Button>();
        nextLevel = GameObject.FindGameObjectWithTag("NextBtn").GetComponent<Button>();
        nextLevel.onClick.RemoveAllListeners();
        nextLevel.onClick.AddListener(() => Reload());//next level butonu Reload fonksiyonu ile iþlev görecek
        nextLevel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Again";//next yazýsýný againe çevirdik
    }

    public void OpenLB()//leader board
    {
        InGame.SetActive(false);
        LeaderBoard.SetActive(true);
        if (GameManager.instance.failed)
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            nextLevelText.text = PlayerPrefs.GetInt("Level", 1) + 1 + "";
            fill.sprite = gray;
        }
        else
        {
            currentLevelText.text = PlayerPrefs.GetInt("Level", 1) - 1 + "";
            nextLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
            fill.sprite = orange;
        }
        

    }
   

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);//main menu index 0'da olacak;
    }
}
