using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private GameObject[] runners;
    public static GameManager instance;
    List<RankingSystem> sortArray = new List<RankingSystem>();
    public bool finish,failed,start;
    public int pass;
    public string firstPlace, secondPlace;
    private InGame ig;

  
    void Awake()
    {
        instance=this;
        runners = GameObject.FindGameObjectsWithTag("Runner");
        ig = FindObjectOfType<InGame>();
    }

    void Start()
    {
        for(int i=0; i<runners.Length;i++)
        {
            sortArray.Add(runners[i].GetComponent<RankingSystem>());
           
        }
    }

    
    void Update()
    {
        CalculatingRank();
    }

    void CalculatingRank()
    {
        sortArray = sortArray.OrderBy(x => x.counter).ToList();
        

        switch(sortArray.Count)//oyuncularý yok ettiðimiz için bu kontrolü saðlýyoruz
        {
            case 2:
                sortArray[0].rank = 2;
                sortArray[1].rank = 1;

                ig.a = sortArray[1].name;
                ig.b = sortArray[0].name;
                break;
            case 1:
                sortArray[0].rank = 1;
                ig.a = sortArray[0].name;
                if (firstPlace=="")
                {
                    firstPlace = sortArray[0].name;
                    print(firstPlace);
                    GameUI.instance.OpenLB();
                }
                break;

        }

        if(pass>=(float)runners.Length/2)//3 oyuncu eklersem 2 oyuncu finis çizgisini geçtiði zaman 3. oyuncuyu yok etmek için buraya girecek
        {
            pass = 0;
            sortArray = sortArray.OrderBy(x => x.counter).ToList();
            
            foreach(RankingSystem rs in sortArray)
            {
                if(rs.rank==sortArray.Count)//dizinin sonuncu elemaný yarýþta sonuncu olacak ona eþit ise
                {
                    if(rs.gameObject.name== PlayerPrefs.GetString("PlayerName"))
                    {                        
                        failed = true;                       
                        GameUI.instance.OpenLB();
                        
                        
                    }
                    if (secondPlace == "")
                        secondPlace = rs.gameObject.name;
                    
                    rs.gameObject.SetActive(false);//bu oyuncuyu yok ediyoruz

                }
                
            }

            runners = GameObject.FindGameObjectsWithTag("Runner");
            sortArray.Clear();

            for (int i = 0; i < runners.Length; i++)
            {
                sortArray.Add(runners[i].GetComponent<RankingSystem>());
               
            }

            if(runners.Length<=1)
            {
                finish = true;
                if(SceneManager.GetActiveScene().buildIndex>= PlayerPrefs.GetInt("Level"))
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level",1) + 1);
            }

        }


    }
}
