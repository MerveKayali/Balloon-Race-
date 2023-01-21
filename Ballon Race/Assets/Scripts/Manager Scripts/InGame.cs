using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame : MonoBehaviour
{
    public TextMeshProUGUI[] namesTxt;
    public string a, b;

    
    void Update()
    {
        namesTxt[0].text = a;
        namesTxt[1].text = b;
        
    }
}
