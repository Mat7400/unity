using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class u1script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var u3 = GameObject.Find("u3");
        //on start make u3 invisible
        // Hide 
        u3.transform.localScale = new Vector3(0, 0, 0);

        
    }
    int count = 0;
    bool opened = false;
    // Update is called once per frame
    void Update()
    {
        count++;
        if (count % 150 == 0 && count> 150)
        {
            if (!opened)
            {
                var u1 = GameObject.Find("u1");
                var u3 = GameObject.Find("u3");
                //animate - make u3 visible and u1 invisible
                //hide u1
                u1.transform.localScale = new Vector3(0, 0, 0);

                // Show u3
                u3.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
