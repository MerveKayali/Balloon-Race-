using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleKnifes : MonoBehaviour
{
    private float RotationSpeed = 150;
    void Start()
    {
        
    }

    private void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }


}
