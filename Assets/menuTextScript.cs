using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuTextScript : MonoBehaviour
{
    /// <summary>
    /// player name from initial scene
    /// </summary>
    public static string playerName="";
    // Start is called before the first frame update
    void Start()
    {
        //get player name from other scene
        var classtext = GameObject.Find("Text").GetComponent<Text>();
        classtext.text = playerName;
        classtext.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
