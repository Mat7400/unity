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
        //get input field
        var input = GameObject.Find("InputField").GetComponent<InputField>();
        string text = input.text;
        //get text element in Unity
        classtext = GameObject.Find("TextTextPlayer").GetComponent<Text>();
        classtext.text = text;
        classtext.color = Color.red;
        Debug.Log("tet setPlayerName");
    }
    public void menubutton()
    {
        var input = GameObject.Find("InputField");
        input.SetActive(false);
        //move
        var cs = GameObject.Find("TextTextPlayer").GetComponent<RectTransform>();
        var vect = cs.offsetMax;
        vect.x = vect.x + 15;
        vect.y = vect.y + 15;
        cs.offsetMax = vect;
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
