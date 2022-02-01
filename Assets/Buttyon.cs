using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttyon : MonoBehaviour
{
    Text classtext;
    // Start is called before the first frame update
    public void setPlayerName()
    {
        string pname = "test";
        //get text element in Unity
        classtext = GameObject.Find("TextPlayerName").GetComponent<Text>();
        classtext.text = pname;
        classtext.color = Color.red;
    }
    public void Start()
    {
        Debug.Log("test");

    }
    public void Update()
    {
        Debug.Log("Update");
    }
}
