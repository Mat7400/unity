using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player
{
    public int Exp = 0;


    public int HealPoints = 15;

    public int Atack = 3;

    public int SuperAtack = 16;


    public string name = "knight";
    public Player(System.Random rnd)
    {
        //set initial hp and damages
        gid = Guid.NewGuid().ToString();
        System.Random rondom = rnd;
        minDamage = rondom.Next(1, 5);
        maxDamage = rondom.Next(8, 12);
        HealPoints = rondom.Next(10, 25);
    }
    public string gid { get; set; }
    public int maxDamage = 10;
    public int minDamage = 1;
    public int CountDamage()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(minDamage, maxDamage);
    }
    //dealed damage array (list)
    //DZ
    //03.01.2021 другие коллекции - очередь Queue, стек Stack и Словарь (dictonary)
    //можно например хранить историю хилпоинтов
    public List<int> dealedDamage = new List<int>();
    //очередь - первый вошел первый ушел
    //например список найденных файлов
    public Queue<int> dd2 = new Queue<int>();
    public void dealdamage(int damage)
    {
        HealPoints = HealPoints - damage;
        //store damage in array
        //fixed array
        //string[] lkka = new string[10];
        //dynamic array
        //List<int> lst = new List<int>();
        dealedDamage.Add(damage);

        
        
    }
    public bool isAlive()
    {
        //return true if alive
        return HealPoints > 0;
    }
}
public class battlePlayer : MonoBehaviour
{

    public static string playerName = "";
    Player player ;
    Player enemy ;
    bool gameend = false;
    // Start is called before the first frame update
    void Start()
    {
        //System.Random rnd = new System.Random();
        //player = new Player(rnd);
        //enemy = new Player(rnd);
        ////TODO: выбор персонажа (либо в меню либо случайно из 3-5 картинок в Ассетах)

        //player.name = playerName;
        //enemy.name = "KULAK";

        ////show heal point to TextDamage
        //var classtext = GameObject.Find("TextDamage").GetComponent<Text>();
        //classtext.text = player.name+" HP=" +player.HealPoints+" -- "+enemy.name+" HP="+enemy.HealPoints;
        //classtext.color = Color.green;
        //gameend = false;
        newgame();
    }
    int count = 0;

    int lefrborder = -30;
    int rightborder = 30;
    int playerspeed = 2;
    int enemyspeed = -2;
    int xplayer = -20;
    int xenemy = 20;
    public void toMenu()
    {
        SceneManager.LoadScene("menu");
    }
        public void newgame()
    {
        System.Random rnd = new System.Random();
        player = new Player(rnd);
        enemy = new Player(rnd); 
        player.name = playerName;
        enemy.name = "KULAK";
        var classtext = GameObject.Find("TextDamage").GetComponent<Text>();
        classtext.text = player.name + " HP=" + player.HealPoints + " -- " + enemy.name + " HP=" + enemy.HealPoints;
        classtext.color = Color.green;
        xplayer = -20;
        playerspeed = 2;
        enemyspeed = -2;
        xenemy = 20;
        //
        var playerObj = GameObject.Find("player2");
        var enemyObj = GameObject.Find("enemy");
        //get coordinates
        var transformP = playerObj.GetComponent<Transform>();
        var transformE = enemyObj.GetComponent<Transform>();
        transformP.position = new Vector3(xplayer, 0, 0);
        transformE.position = new Vector3(xenemy, 0, 0);
        gameend = false;
        //player2 change sprite to ava2-ava6
        //1)get sprite from assets
        //2)set sprite to playerObj
        int ava = rnd.Next(2, 6);
        var sp = Resources.Load("ava"+ava.ToString().Trim()) as Sprite;
        var render = playerObj.GetComponent<SpriteRenderer>();
        render.sprite = sp;
        //if (ava==2)
        //{
        //    var sp = Resources.Load("ava2") as Sprite;
        //    var render = playerObj.GetComponent<SpriteRenderer>();
        //    render.sprite = sp;
        //}
        //else if (ava==3)
        //{
        //    var sp = Resources.Load("ava3") as Sprite;
        //    var render = playerObj.GetComponent<SpriteRenderer>();
        //    render.sprite = sp;
        //}
        //else if (ava == 4)
        //{ }
        //else if (ava == 5)
        //{ }
        //else if (ava == 6)
        //{ }
    }
    // Update is called once per frame
    void Update()
    {
        //animate player and enemy
        //60fps - once in 1s
        if (count % 60 == 0 && gameend==false)
        {
            var playerObj = GameObject.Find("player2");
            var enemyObj = GameObject.Find("enemy");
            //get coordinates
            var transformP = playerObj.GetComponent<Transform>();
            var transformE = enemyObj.GetComponent<Transform>();
            //transform2.position += new Vector3(30, 30, 0);
            //translate - двигает из того места где уже был

            transformP.Translate(playerspeed, 0, 0);
            transformE.Translate(enemyspeed, 0, 0);
            //change x coord
            xplayer = xplayer + playerspeed;
            xenemy = xenemy + enemyspeed;
            //if not collide - move them

            //if border - change speed
            if (xenemy >= rightborder || xenemy <= lefrborder)
                //change speed
                enemyspeed = enemyspeed * (-1);
            if (xplayer >= lefrborder || xplayer <= lefrborder)
                //change speed
                playerspeed = playerspeed * (-1);


            //if collide - count damage, change speed
            //
            if ( (xplayer - xenemy < 6) || xplayer > xenemy)
            {
                //change speed
                playerspeed = playerspeed * (-1);
                enemyspeed = enemyspeed * (-1);
                //change x coord
                xplayer = xplayer + playerspeed;
                xenemy = xenemy + enemyspeed;

                //random damage
                var dmage1 = player.CountDamage();
                var dmg2 = enemy.CountDamage();
                player.dealdamage(dmg2);
                enemy.dealdamage(dmage1);
                var classtext = GameObject.Find("TextDamage").GetComponent<Text>();
                if (player.isAlive() == false && enemy.isAlive() == false)
                {
                    //listbox - массив dealedDamage вывести

                    classtext.text = "TIE play";
                    //stop update
                    gameend = true;
                }
                else
                {
                    if (player.isAlive() == false)
                    {

                        classtext.text = "player LOSE";
                        //stop update
                        gameend = true;

                    }
                    else if (enemy.isAlive() == false)
                    {

                        classtext.text = "player WIN";
                        //stop update
                        gameend = true;

                    }
                    else
                    {
                        //show damage on TextDamage text field

                        classtext.text = player.name + " HP=" + player.HealPoints + " -- " + enemy.name + " HP=" + enemy.HealPoints;
                    }
                }
            }
            
        }
        count++;
    }
}
