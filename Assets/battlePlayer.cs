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
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);

        //System.Random rnd = new System.Random();
        //player = new Player(rnd);
        //enemy = new Player(rnd);
        // TODO: выбор персонажа (либо в меню 
        // V либо случайно из 3-5 картинок в Ассетах)

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

    int lefrborder = -20;
    int rightborder = 20;
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
    /// <summary>
    /// 120 for PC, 30 for android
    /// </summary>
    int animationSpeed = 120;
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
        // поискать как отслеживать столкновение именно GameObject в игре.
        //наверняка есть события которые можно отследить.
        //
        //12.03 еще осталось сделать выбор игрока на экране меню.
        //чтоб рандомный персонаж вылетал из "сундука".
        //и фоны поля битвы разные рандомные
        //
        //13.03 отследить запуск на андриде и ПК и поставить разные скорости анимации
        //TODO сделать лидерборд с кубками и сохранять его в текст\SQL
        //
        //15.03 сервер с лидербордом онлайн
        //у нас есть два строковых поля и 4 числа (победы, поражения, сумма урона от Игрока и от Врага)
        //нужен простой сервис хранения этих данных и ведения лидерборда.
        //берем AirTable http API https://airtable.com/api/meta
        //
        //вопрос: доступ в интернет для андроид приложения. сможет ли оно отправить данные http?
        //
        

        bool platformAndroid = false;
        
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            platformAndroid = true;
        //более быстрая скорость анимации на телефоне
        if (platformAndroid) animationSpeed = 30;

        if (gameend == false)
        {
            if (count % animationSpeed == 0)
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
                        //лидерборд
                        SaveLeaderBoard(player, enemy, "TIE");
                        
                    }
                    else if (player.HealPoints <= 0 && enemy.HealPoints > 0)
                    {

                        classtext.text = "player LOSE"
                            + " LAST HP Pl=" + lsthpp + " en=" + lsthpe + " Dmg1=" + dmage1 + " dmg2=" + dmg2;
                        //stop update
                        gameend = true;
                        SaveLeaderBoard(player, enemy, "LOSE");
                    }
                    else if (enemy.HealPoints <= 0 && player.HealPoints > 0)
                    {

                        classtext.text = "player WIN"
                            + " LAST HP Pl=" + lsthpp + " en=" + lsthpe + " Dmg1=" + dmage1 + " dmg2=" + dmg2;
                        //stop update
                        gameend = true;
                        SaveLeaderBoard(player, enemy, "WIN");
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

    private void SaveLeaderBoard(Player player, Player enemy, string v)
    {
        airtable.AirTableSaver saver = new airtable.AirTableSaver();
        saver.saveToCloud(player, enemy, 1, 1);
        //save to text
        //нужно разрешение для создания файлов на Андроид?
        string result = "";
        int sumplayer = 0;
        foreach (var item in player.dealedDamage)
        {
            sumplayer = sumplayer + item;
        }
        int sumnemy = 0;
        foreach (var item in enemy.dealedDamage)
        {
            sumnemy = sumnemy + item;
        }
        result = "Player dealedDamage="+sumplayer+" enemy="+ sumnemy+" res="+v;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //permission
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageWrite))
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageWrite);
                //UnityEngine.Android.Permission.RequestUserPermission("android.permission.WRITE_EXTERNAL_STORAGE");
                //create text (не работает на Андроид без разрешения доступа к файловой системе!)
                string tempPath = System.IO.Path.Combine(Application.persistentDataPath, "clashscore.txt");
                System.IO.File.AppendAllText(tempPath, result);
            }
            
        }
        else
        {
            //create text (не работает на Андроид без разрешения доступа к файловой системе!)
            string tempPath = System.IO.Path.Combine(Application.persistentDataPath, "clashscore.txt");
            System.IO.File.AppendAllText(tempPath, result);
        }
       
    }
}
