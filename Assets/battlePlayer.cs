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
        minDamage = rondom.Next(minDamageLeft, minDamageRight);
        maxDamage = rondom.Next(maxDamageLeft, maxDamageRight);
        HealPoints = rondom.Next(minHP, maxHP);
        name = "PLAYER";
        dealedDamage = new List<int>();
    }
    public string gid { get; set; }

    public int maxDamage = 10;
    public int minDamage = 1;
    //12.03 шесть параметров которые регулируют игру
    //диапазон минимального урона
    public int minDamageLeft = 3;
    public int minDamageRight = 6;
    //диапазон максимального урона
    public int maxDamageLeft = 9;
    public int maxDamageRight = 13;
    //здоровье
    public int maxHP = 40;
    public int minHP = 20;
    public int CountDamage(System.Random rnd)
    {

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
        //return true if alive - ХП больше 0
        return (HealPoints > 0);
    }
}
public class battlePlayer : MonoBehaviour
{

    public static string playerName = "";
    Player player;
    Player enemy;
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

    int lefrborder = -25;
    int rightborder = 25;
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
        if (playerName != "")
            player.name = playerName;

        enemy.name = "KULAK";
        var classtext = GameObject.Find("TextDamage").GetComponent<Text>();
        classtext.text = player.name + " HP=" + player.HealPoints + " -- " + enemy.name + " HP=" + enemy.HealPoints;
        classtext.color = Color.green;
        //начальные Х координаты отрегулировать под анимацию игры
        xplayer = -10;
        playerspeed = 2;
        enemyspeed = -2;
        xenemy = 10;
        //
        var playerObj = GameObject.Find("player2");
        var enemyObj = GameObject.Find("enemy");
        //get coordinates
        var transformP = playerObj.GetComponent<Transform>();
        var transformE = enemyObj.GetComponent<Transform>();
        //09.03 позиция на +20 -20
        transformP.position = new Vector3(-20, 0, 0);
        transformE.position = new Vector3(20, 0, 0);
        gameend = false;
        //player2 change sprite to ava2-ava6
        //1)get sprite from assets
        //2)set sprite to playerObj
        int ava = rnd.Next(2, 6);
        // Assets/ava4.png 
        //Assets / ava3.jpg
        //Assets/ava2.jpg
        //Assets / ava3.jpg
        //Assets / ava5.png
        //Assets/ava6.png

        if (ava == 2)
        {
            //Assets / Resources / ava2.jpg
            loadSprite("ava2", playerObj);
        }
        else if (ava == 3)
        {
            //Assets/Resources/ava2.jpg
            loadSprite("ava3", playerObj);
        }
        else if (ava == 4)
        {
            loadSprite("ava4", playerObj);
        }
        else if (ava == 5)
        {
            loadSprite("ava5", playerObj);
        }
        else if (ava == 6)
        {
            loadSprite("ava6", playerObj);
        }
    }
    public void loadSprite(string name, GameObject playerObj)
    {
        //06.03 ошибка загрузки спрайтов в этой строке
        //нужно создать папку Resources
        //не помогло
        //09.03 помогло вот что находим ВСЕ ресурсы с именем
        //и в цикле проверяем получилось преобразование в спрайт или нет
        var resall = Resources.LoadAll(name);
        string ress = "";
        foreach (var obj in resall)
        {
            //ress = ress +" _ "+ obj.name;
            var sp = obj as Sprite;
            if (sp != null)
            {
                var render = playerObj.GetComponent<SpriteRenderer>();
                render.sprite = sp;

            }
            else
            {
                //Debug.Log("NO SPRITE " + name);
            }
        }
        //Debug.Log("SPRITE TEST " + ress);
        //var spp = Resources.Load(name);

    }
    // Update is called once per frame
    void Update()
    {
        //animate player and enemy
        //60fps - once in 1s
        //09.03 замечено что по прошествии времени анимация расходитя с расчетами
        //то есть в анимации Карыч (игрок) улетает каждый раз все правее и правее Врага
        //и расчетное столкновение (когда Х равны) в анимации выглядит как 
        //Игрок (изображение) сильно правее Врага
        //
        //TODO: поискать как отслеживать столкновение именно GameObject в игре.
        //наверняка есть события которые можно отследить.
        //
        //12.03 еще осталось сделать выбор игрока на экране меню.
        //чтоб рандомный персонаж вылетал из "сундука".
        //и фоны поля битвы разные рандомные
        //
        if (gameend == false)
        {
            if (count % 120 == 0)
            {
                var playerObj = GameObject.Find("player2");
                var enemyObj = GameObject.Find("enemy");
                //get coordinates
                var transformP = playerObj.GetComponent<Transform>();
                var transformE = enemyObj.GetComponent<Transform>();
                //transform2.position += new Vector3(30, 30, 0);
                //translate - двигает из того места где уже был


                //12.03 движение объектов
                transformP.Translate(playerspeed, 0, 0);
                transformE.Translate(enemyspeed, 0, 0);
                //12.03 получить координаты Объектов Игрока и Врага и проверить равенство
                bool unityCoordEquals = false;

                unityCoordEquals = (playerObj.transform.position.x == enemyObj.transform.position.x);
                //change x coord
                xplayer = xplayer + playerspeed;
                xenemy = xenemy + enemyspeed;
                //if not collide - move them

                //if border - change speed
                if (xenemy >= rightborder || xenemy <= lefrborder)
                    //change speed
                    enemyspeed = enemyspeed * (-1);
                //06.03 было неверное условие!
                if (xplayer <= lefrborder || xplayer >= rightborder)
                    //change speed
                    playerspeed = playerspeed * (-1);

                var classtext = GameObject.Find("TextDamage").GetComponent<Text>();
                classtext.text = player.name + " HP=" + player.HealPoints + " -- "
                    + enemy.name + " HP=" + enemy.HealPoints;
                //if collide - count damage, change speed
                //06.03 тщательнее прописать условие столкновения!
                //Игрок летит лева направо, его Х меньше 0
                //так как х Игрока должен быть меньше Х Врага - то столкновение это когда Х равны
                //и тогда надо поменять скорости в другом направлении и 
                //и сделать "разлет" - то есть игрок летит влево враг Вправо
                //if (xplayer == xenemy || unityCoordEquals==true)
                if (unityCoordEquals == true && gameend == false)
                {
                    if (playerspeed > 0 && enemyspeed < 0)
                    {
                        //change speed
                        playerspeed = playerspeed * (-1);
                        enemyspeed = enemyspeed * (-1);
                        //change x coord
                        xplayer = xplayer + playerspeed;
                        xenemy = xenemy + enemyspeed;
                        //12.03 движение объектов
                        transformP.Translate(playerspeed, 0, 0);
                        transformE.Translate(enemyspeed, 0, 0);
                    }
                    else
                    {
                        //change x coord
                        xplayer = xplayer + playerspeed;
                        xenemy = xenemy + enemyspeed;
                        //12.03 движение объектов
                        transformP.Translate(playerspeed, 0, 0);
                        transformE.Translate(enemyspeed, 0, 0);
                    }


                    //random damage
                    System.Random rnd = new System.Random();
                    var dmage1 = player.CountDamage(rnd);
                    var dmg2 = enemy.CountDamage(rnd);
                    int lsthpp = player.HealPoints;
                    int lsthpe = enemy.HealPoints;
                    player.dealdamage(dmg2);
                    enemy.dealdamage(dmage1);

                    if (player.HealPoints <= 0 && enemy.HealPoints <= 0)
                    {
                        //listbox - массив dealedDamage вывести

                        classtext.text = "TIE play"
                            + " LAST HP Pl=" + lsthpp + " en=" + lsthpe + " Dmg1PE=" + dmage1 + " dmg2EP=" + dmg2;
                        //stop update
                        gameend = true;
                    }
                    else if (player.HealPoints <= 0 && enemy.HealPoints > 0)
                    {

                        classtext.text = "player LOSE"
                            + " LAST HP Pl=" + lsthpp + " en=" + lsthpe + " Dmg1=" + dmage1 + " dmg2=" + dmg2;
                        //stop update
                        gameend = true;

                    }
                    else if (enemy.HealPoints <= 0 && player.HealPoints > 0)
                    {

                        classtext.text = "player WIN"
                            + " LAST HP Pl=" + lsthpp + " en=" + lsthpe + " Dmg1=" + dmage1 + " dmg2=" + dmg2;
                        //stop update
                        gameend = true;

                    }
                    else
                    {
                        //show damage HP on TextDamage text field
                        //оба живые

                    }


                }

            }
        }
        count++;
    }
}
