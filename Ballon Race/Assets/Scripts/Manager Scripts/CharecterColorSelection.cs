using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharecterColorSelection : MonoBehaviour
{
    private Camera camreMain;
    private int currentPlayer = 0;

    public float speed = 0.5f;
    public float selectionPos = 13;

    public GameObject parent;

   
    void Start()
    {
        camreMain = Camera.main;
        CameraPos();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CameraPos()
    {
        currentPlayer = PlayerPrefs.GetInt("PlayerColor");
        camreMain.transform.position = new Vector3(camreMain.transform.position.x + (currentPlayer * 13), camreMain.transform.position.y, camreMain.transform.position.z);
    }

    public void Next()
    {
        if(currentPlayer<parent.transform.childCount-1)
        {
            currentPlayer++;
            StartCoroutine(MoveToNext());
        }
    }

    public void Prev()
    {
        if (currentPlayer > 0)
        {
            currentPlayer--;
            StartCoroutine(MoveToPrev());
        }
    }

    IEnumerator MoveToNext()
    {
        Vector3 tempPos = new Vector3(camreMain.transform.position.x + selectionPos, camreMain.transform.position.y, camreMain.transform.position.z);
        while(camreMain.transform.position.x<tempPos.x)
        {
            camreMain.transform.position = Vector3.MoveTowards(camreMain.transform.position, tempPos, speed);
            yield return new WaitForSeconds(Time.deltaTime * speed);
                 
        }
        yield return null;
    }
    IEnumerator MoveToPrev()
    {
        Vector3 tempPos = new Vector3(camreMain.transform.position.x - selectionPos, camreMain.transform.position.y, camreMain.transform.position.z);
        while (camreMain.transform.position.x > tempPos.x)
        {
            camreMain.transform.position = Vector3.MoveTowards(camreMain.transform.position, tempPos, speed);
            yield return new WaitForSeconds(Time.deltaTime * speed);

        }
        yield return null;
    }

    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level",2));
        PlayerPrefs.SetInt("PlayerColor", currentPlayer);
    }
}
