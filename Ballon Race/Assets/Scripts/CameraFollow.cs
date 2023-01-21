using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float smoothSpeed = 0.125f;


    // Update is called once per frame
    void Update()
    {
       // offset.x = player.forward.x * 2.5f;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x + offset.x,
        player.position.y + offset.y, player.position.z+2f + offset.z),50*Time.deltaTime);      
               
    }
    
  



}
