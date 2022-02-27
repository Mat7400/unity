using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadBattle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void menubutton()
    {
        //кнопка слить кубки
            //set menuTextScript.
            battlePlayer.playerName = menuTextScript.playerName;
            SceneManager.LoadScene("Battle");
        

    }
}
