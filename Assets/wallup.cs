using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionStay2D(Collision2D col)
    {

        if (col.gameObject.tag == "InputField")
        {
            Debug.Log("UP WALL HIT");
            throw new Exception("UP WALL HI");
        }
    }
}
