using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] checkPoints;
    [HideInInspector]
    public int currentCheckPoint=1;
    void Awake()
    {
        checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        currentCheckPoint = 1;
    }

   
    void Start()
    {
        foreach(GameObject cp in checkPoints)
        {
            cp.AddComponent<CurrentCheckPoints>();
            cp.GetComponent<CurrentCheckPoints>().currentCheckNumber = currentCheckPoint;
            cp.name = "CheckPoint" + currentCheckPoint;
            currentCheckPoint++;
        }
        
    }
}
