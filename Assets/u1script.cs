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
        //u3.SetActive(false);
        // opacity
        t1 = GameObject.Find("u1").GetComponent<Transform>();
        t2 = GameObject.Find("u3").GetComponent<Transform>();
    }
    Transform t1;
    Transform t2;
    Vector3 tempPos1;
    Vector3 tempPos2;
    int count = 0;
    bool opened = false;
    // Update is called once per frame
    void Update()
    {
        count++;
        if (count % 150 == 0 && count > 150)
        {
            if (!opened)
            {
                tempPos1 = t1.position;
                tempPos2 = t2.position;
                StartCoroutine(Swap(20));
                //animate - make u3 visible and u1 invisible
                //hide u1
                //u1.transform.localScale = new Vector3(0, 0, 0);

                // Show u3
                //u3.transform.localScale = new Vector3(1, 1, 1);
                opened = true;
            }
        }
    }
    IEnumerator Swap(float time)
    {
        float i = 0;
        // opacity
        //1) первая картинка становится все прозрачнее плавно в цикле
        //2) вторая становится плотнее и заменяет первую
        // 3) далее из второй картика появляется "карыч" - герой. 
        while (i < 1)
        {
            i += Time.deltaTime / time;
            t1.position = Vector3.Lerp(t1.position, tempPos2, i);
            t2.position = Vector3.Lerp(t1.position, tempPos1, i);
            yield return 0;
        }
        var u1 = GameObject.Find("u1");
        var u3 = GameObject.Find("u3");
        //u1.SetActive(false);
        //hide u1
        u1.transform.localScale = new Vector3(0, 0, 0);

        // Show u3
        u3.transform.localScale = new Vector3(1, 1, 1);
        //u3.SetActive(true);
    }

}
