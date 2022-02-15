using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Buttyon : MonoBehaviour
{
    int counter = 0;
    Text classtext;
    string playerName = "";
    // Start is called before the first frame update
    public void setPlayerName()
    {
        //get input field
        var input = GameObject.Find("InputField").GetComponent<InputField>();
        string text = input.text;
        playerName = text;
        //get text element in Unity
        classtext = GameObject.Find("TextTextPlayer").GetComponent<Text>();
        classtext.text = text;
        classtext.color = Color.red;
        Debug.Log("tet setPlayerName");
    }

    public void menubutton()
    {
        //15.02 find scene "menu"
        //switch scenes in game mode
        //SceneManager.UnloadScene("SampleScene");
        if (playerName == string.Empty || string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Enter name!");
        }
        else
        {
            //set 
            menuTextScript.playerName = playerName;
            SceneManager.LoadScene("menu");
        }

    }
    public void oldcode()
    {
        var input = GameObject.Find("InputField");
        if (input != null)
            input.SetActive(false);
        //move
        //var cs = GameObject.Find("TextTextPlayer").GetComponent<RectTransform>();
        //var vect = cs.offsetMax;
        //vect.x = vect.x + 15;
        //vect.y = vect.y + 15;
        //cs.offsetMax = vect;

        //move karych
        var karychO = GameObject.Find("KARYCH");
        var transform = karychO.GetComponent<Transform>();
        //position - двигает в координаты вектора
        //transform.position += new Vector3(30, 15, 0);
        //КАРЫЧА ДВИГАТЬ НА ЧУТЬ_ЧУТЬ зависит от размера
        transform.Translate(3, 3, 0);

        var transform2 = GameObject.Find("TextTextPlayer").GetComponent<Transform>();
        //transform2.position += new Vector3(30, 30, 0);
        //translate - двигает из того места где уже был
        transform2.Translate(40, 40, 0);

        var obj = GameObject.Find("TextTextPlayer");
        obj.SetActive(true);



        if (obj.activeSelf)
        {

            Debug.Log("BUTTON OK ACTIVE " + counter);
        }
        else
        {
            Debug.Log("BUTTON OK " + counter);
        }
        counter++;
    }
    public void Start()
    {
        Debug.Log("test");

    }
    float speed = 1;
    public void Update()
    {
        var transform2 = GameObject.Find("TextTextPlayer").GetComponent<Transform>();
        //transform2.position += new Vector3(30, 30, 0);
        //translate - двигает из того места где уже был
        transform2.Translate(Vector3.forward * speed * Time.deltaTime);
        Debug.Log("Update");
    }
}
