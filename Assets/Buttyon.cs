using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttyon : MonoBehaviour
{
    int counter = 0;
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
        if (input!=null)
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
        Debug.Log("BUTTON OK "+ counter);
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
