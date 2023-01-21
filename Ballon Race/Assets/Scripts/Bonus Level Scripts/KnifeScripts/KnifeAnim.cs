using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAnim : MonoBehaviour
{
    Vector3 pos;
    Quaternion quat;
    private float RotationSpeed = 50f;
    public GameObject patlama;
    private AudioSource popSound;



    void Start()
    {
        pos = transform.position;
        quat = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Knife());
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
        

    }

    IEnumerator Knife()
    {     
        if (BonusPlayerController.instance.knifeCounter == 6)
        {                                   
            yield return new WaitForSeconds(1.5f);
            transform.Rotate(pos, RotationSpeed * Time.deltaTime, 0);
            pos = new Vector3(pos.x, pos.y, pos.z + 2f);                   
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90,0,0), 0.1f);
            yield return new WaitForSeconds(1.5f);
            Destroy(GameObject.Find("BigBalloon"));
            
            yield return new WaitForSeconds(0.1f);
            patlama.gameObject.SetActive(true);

        }
        transform.position = Vector3.Lerp(transform.position, pos,0.02f);
       



    }

    

}
