using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kar2 : MonoBehaviour
{
    int speedX = 1;
    int speedY = 1;
    int curX = 0;
    int curY = 0;

    int RightBorder = 9;
    int LeftBorder = -9;
    int UpBorder = 9;
    int DownBorder = -9;

    System.Random randomizer = new System.Random();
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("KAR KAR");
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count % 20 == 0)
        {
            
            var transform2 = GameObject.Find("KARYCH").GetComponent<Transform>();
            //transform2.position += new Vector3(30, 30, 0);
            //translate - двигает из того места где уже был

            transform2.Translate(speedX, speedY, 0);

            curX = curX + speedX;
            curY = curY + speedY;

            if (curX > RightBorder)
            {
                speedX = randomizer.Next(1, 3);
                if (speedX > 0)
                    speedX = (-1) * speedX;

            }
            if (curX < LeftBorder)
            {
                speedX = randomizer.Next(1, 3);
                if (speedX < 0)
                    speedX = (-1) * speedX;
            }
            if (curY < DownBorder)
            {
                speedY = randomizer.Next(1, 3);
                if (speedY < 0)
                    speedY = (-1) * speedY;

            }

            if (curY > UpBorder)
            {
                speedY = randomizer.Next(1, 3);
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
