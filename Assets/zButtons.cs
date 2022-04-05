using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // допустим хотим двигать объект "image 3" по нажатию клавиш WASD
        // по нажатию кнопки прилетает оповещение что кнопка нажата в Винду (ОС)
        // у кнопки есть код допустим что клавиша А это код 044
        // приложениям надо отслеживать эти оповещения и понимать какая клавиша нажата
        // 
        // На сенсорном экране телефона есть координаты и есть координаты нажатия на экран
        // можно например сделать область управления (круг) и если координаты нажатия
        // попали в сектор круга "влево" то и объект едет влево
        // или сделать 4 кнопки по нажатию на которых отслеживать управление
        //
        // если игроком можно управлять и летать им 
        // то кулак должен летать за ним 
        // и столкновения происходят только если Х и У координаты равны

    }
    public void upClick()
    {
        var gobject = GameObject.Find("player2");
        var transf = gobject.GetComponent<Transform>();
        int speed = 1;
        transf.Translate(0, speed, 0);
    }
    public void downClick()
    {
        var gobject = GameObject.Find("player2");
        var transf = gobject.GetComponent<Transform>();
        int speed = 1;
        transf.Translate(0, -speed, 0);
    }
    public void leftClick()
    {
        var gobject = GameObject.Find("player2");
        var transf = gobject.GetComponent<Transform>();
        int speed = 1;
        transf.Translate(-0.5f, 0, 0);
        //todo сделать во вторник 05.04
    }
    public void rightClick()
    {
        var gobject = GameObject.Find("player2");
        var transf = gobject.GetComponent<Transform>();
        int speed = 1;
        transf.Translate(0.5f, 0, 0);
        //todo сделать во вторник 05.04
    }
    // Update is called once per frame
    void Update()
    {
        var gobject = GameObject.Find("image 3");
        var transf = gobject.GetComponent<Transform>();
        int speed = 1;
        //
        if (Input.GetKeyDown(KeyCode.W))
        {
            //w up
            transf.Translate(0, speed, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //s down
            transf.Translate(0, -speed, 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //A left
            transf.Translate(-speed, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //D right
            transf.Translate(speed, 0, 0);
        }
    }
}
