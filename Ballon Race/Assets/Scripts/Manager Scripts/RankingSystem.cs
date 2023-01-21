using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
   
    //public List<GameObject> ballons = new List<GameObject>();
    public int currectCheckPoint = 1, lapCount;

    public float distance;
    private Vector3 checkPoint;

    public float counter;
    public int rank,score=0;

    void Start()
    {
        currectCheckPoint = 1;
        checkPoint = GameObject.Find("CheckPoint" + currectCheckPoint).transform.position;
    }

    void Update()
    {
        CalculateDistance();
    }

    void CalculateDistance()
    {
        for (int i = 0; i < 5; i++)
        {
            if (PlayerController.instance.ballons[i]== null)
            {
                score += 1;
            }

        }
        distance = Vector3.Distance(transform.position, checkPoint);
        counter = currectCheckPoint * 100 + distance;
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "CheckPoint")
        {
            currectCheckPoint = target.GetComponent<CurrentCheckPoints>().currentCheckNumber;
            checkPoint = GameObject.Find("CheckPoint" + currectCheckPoint).transform.position;
        }

        if (target.tag == "Finish")
        {
            lapCount += 1;
            GameManager.instance.pass += 1;
        }
    }
}
/*for (int i = 0; i < 5; i++)
{
    if (ballons[i] == null)
    {
        score += 1;
    }

}*/