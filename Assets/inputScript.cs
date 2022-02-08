using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class inputScript : MonoBehaviour
{
    int speedX = 1;
    int speedY = 1;
    int curX = 0;
    int curY = 0;

    int RightBorder = 45;
    int LeftBorder = -45;
    int UpBorder = 45;
    int DownBorder = -45;

    System.Random randomizer = new System.Random();
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
       
        curX = 0;
        curY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        count++;
        if (count % 20 == 0)
        {
            Debug.Log("UPDATE input " + count + " curX=" + curX + " curY=" + curY
                 + " speedX=" + speedX + " speedY=" + speedY);
            var transform2 = GameObject.Find("TextTextPlayer").GetComponent<Transform>();
            //transform2.position += new Vector3(30, 30, 0);
            //translate - двигает из того места где уже был

            transform2.Translate(speedX, speedY, 0);

            curX = curX + speedX;
            curY = curY + speedY;

            if (curX > RightBorder)
            {
                speedX = randomizer.Next(1, 4);
                if (speedX > 0)
                    speedX = (-1) * speedX;
                
            }
            if (curX < LeftBorder)
            {
                speedX = randomizer.Next(1, 4);
                if (speedX < 0)
                    speedX = (-1) * speedX;
            }
            if (curY < DownBorder)
            {
                speedY = randomizer.Next(1, 4);
                if (speedY < 0)
                    speedY = (-1) * speedY;
                
            }

            if (curY > UpBorder)
            {
                speedY = randomizer.Next(1, 4);
                if (speedY > 0)
                    speedY = (-1) * speedY;
            }

            //borders
            var wall1 = GameObject.Find("WallLeft");
            var wall2 = GameObject.Find("WallDown");
            var wall3 = GameObject.Find("WallUp");
            var wall4 = GameObject.Find("WallRight");
        }
    }
}
