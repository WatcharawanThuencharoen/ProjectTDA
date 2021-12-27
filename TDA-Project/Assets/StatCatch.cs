using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System.Text;

//[System.Serializable]
public class StatCatch : MonoBehaviour
{
    public static StatCatch instance;
    public static string playerProfile;
    public static bool inMenu;

    public static int hr = 0, min = 0;
    public static float sec = 0;
    public static int lhr = 0, lmin = 0;
    public static float lsec = 0;
    public static int hhr = 0, hmin = 0;
    public static float hsec = 0;

    public static int stageCount = 0;
    public static string username, password;
    public static int hp = 50, atk = 0, def = 0, weaponatk = 20, lastLocation = 0, nextLocation = 1; //playerStat
    public static string weapon = "Sword";
    public static float goblinhp, goblinatk, goblindef; // Goblin Stat
    public static float slimehp, slimeatk, slimedef; // Slime Stat
    public static float minohp, minoatk, minodef; // Minotaur Stat

    public static int onegamekill = 0, mostonegamekill = 0, goblinkill = 0, mostgoblinkill = 0, slimekill = 0, mostslimekill = 0, totalgoblinkill = 0, totalslimekill = 0, totalminokill = 0, totalplay = 0, totalclear = 0, moststageclear = 0, lessstageclear = 0;
    public static bool minokilled = false, playerdead = false;

    public static string line = "", settingline = "", path = "", settingpath = "";
    public static float volume = 0f;
    public static List<string> lines = new List<string>();
    public static List<string> settinglines = new List<string>();


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void updateTime()
    {
        hr = timecount.Hours;
        min = timecount.Minutes;
        sec = timecount.Seconds;
    }

    public static void setTime()
    {
        timecount.Hours = hr;
        timecount.Minutes = min;
        timecount.Seconds = sec;
    }

    public static void EndStageUpdate()
    {
        updateTime();
        stageCount = StageCount.count;
        updatePlayerStat();
        goblinhp = GoblinAI.hp;
        goblinatk = GoblinAI.atk;
        goblindef = GoblinAI.def;
        slimehp = SlimeAI.hp;
        slimeatk = SlimeAI.atk;
        slimedef = SlimeAI.def;
        minohp = MinotaurAI.hp;
        minoatk = MinotaurAI.atk;
        minodef = MinotaurAI.def;

        setLocation();

    }


    public static void setPlayerStat()
    {
        PlayerMovement.hp = hp;
        PlayerMovement.atk = atk;
        PlayerMovement.def = def;
        PlayerMovement.curweapon = weapon;
        PlayerMovement.weaponatk = weaponatk;
    }
    public static void updatePlayerStat()
    {
        hp = PlayerMovement.hp;
        atk = PlayerMovement.atk;
        def = PlayerMovement.def;
        weapon = PlayerMovement.curweapon;
        weaponatk = PlayerMovement.weaponatk;
        //Debug.Log("Updater Stat Call  DMG:" + weapon);
    }

    public static void updateMonsterStat()
    {
        goblinhp += goblinhp * (5.0f / 100.0f);
        goblinatk += goblinatk * (5.0f / 100.0f);
        goblindef += goblindef * (5.0f / 100.0f);
        slimehp += slimehp * (5.0f / 100.0f);
        slimeatk += slimeatk * (5.0f / 100.0f);
        slimedef += slimedef * (5.0f / 100.0f);
        minohp += minohp * (5.0f / 100.0f);
        minoatk += minoatk * (5.0f / 100.0f);
        minodef += minodef * (5.0f / 100.0f);
        //GoblinAI.hp = (int)Mathf.Floor(goblinhp);
        //GoblinAI.atk = (int)Mathf.Floor(goblinatk);
        //GoblinAI.def = (int)Mathf.Floor(goblindef);
        //SlimeAI.hp = (int)Mathf.Floor(slimehp);
        //SlimeAI.atk = (int)Mathf.Floor(slimeatk);
        //SlimeAI.def = (int)Mathf.Floor(slimedef); 
    }
    public static void calculateMonsterStat()
    {
        //Goblin
        if (goblinatk % 1 >= 0.5)
        {
            GoblinAI.atk = (int)Mathf.Ceil(goblinatk);
        }
        else if (goblinatk % 1 < 0.5)
        {
            GoblinAI.atk = (int)Mathf.Floor(goblinatk);
        }
        if (goblindef % 1 >= 0.5)
        {
            GoblinAI.def = (int)Mathf.Ceil(goblindef);
        }
        else if (goblindef % 1 < 0.5)
        {
            GoblinAI.def = (int)Mathf.Floor(goblindef);
        }
        if (goblinhp % 1 >= 0.5)
        {
            GoblinAI.hp = (int)Mathf.Ceil(goblinhp);
        }
        else if (goblinhp % 1 < 0.5)
        {
            GoblinAI.hp = (int)Mathf.Floor(goblinhp);
        }

        //Slime
        if (slimeatk % 1 >= 0.5)
        {
            SlimeAI.atk = (int)Mathf.Ceil(slimeatk);
        }
        else if (slimeatk % 1 < 0.5)
        {
            SlimeAI.atk = (int)Mathf.Floor(slimeatk);
        }
        if (slimedef % 1 >= 0.5)
        {
            SlimeAI.def = (int)Mathf.Ceil(slimedef);
        }
        else if (slimedef % 1 < 0.5)
        {
            SlimeAI.def = (int)Mathf.Floor(slimedef);
        }
        if (slimehp % 1 >= 0.5)
        {
            SlimeAI.hp = (int)Mathf.Ceil(slimehp);
        }
        else if (slimehp % 1 < 0.5)
        {
            SlimeAI.hp = (int)Mathf.Floor(slimehp);
        }


        //Minotaur
        if (minoatk % 1 >= 0.5)
        {
            MinotaurAI.atk = (int)Mathf.Ceil(minoatk);
        }
        else if (minoatk % 1 < 0.5)
        {
            MinotaurAI.atk = (int)Mathf.Floor(minoatk);
        }
        if (minodef % 1 >= 0.5)
        {
            MinotaurAI.def = (int)Mathf.Ceil(minodef);
        }
        else if (minodef % 1 < 0.5)
        {
            MinotaurAI.def = (int)Mathf.Floor(minodef);
        }
        if (minohp % 1 >= 0.5)
        {
            MinotaurAI.hp = (int)Mathf.Ceil(minohp);
        }
        else if (minohp % 1 < 0.5)
        {
            MinotaurAI.hp = (int)Mathf.Floor(minohp);
        }
    }

    private static void setLocation()
    {
        if (PlayerMovement.lastLocation.x < -300 && PlayerMovement.lastLocation.x < 300 && PlayerMovement.lastLocation.y > -160 && PlayerMovement.lastLocation.y < 160)
        {
            lastLocation = 0;
            nextLocation = 1;
        }
        else if (PlayerMovement.lastLocation.x > -300 && PlayerMovement.lastLocation.x > 300 && PlayerMovement.lastLocation.y > -160 && PlayerMovement.lastLocation.y < 160)
        {
            lastLocation = 1;
            nextLocation = 0;
        }
        else if (PlayerMovement.lastLocation.x > -300 && PlayerMovement.lastLocation.x < 300 && PlayerMovement.lastLocation.y < -160 && PlayerMovement.lastLocation.y < 160)
        {
            lastLocation = 2;
            nextLocation = 3;
        }
        else if (PlayerMovement.lastLocation.x > -300 && PlayerMovement.lastLocation.x < 300 && PlayerMovement.lastLocation.y > -160 && PlayerMovement.lastLocation.y > 160)
        {
            lastLocation = 3;
            nextLocation = 2;
        }
    }

    public static void writeFile()
    {
        string content = username + "\n" + //0
                        password + "\n" + //1
                                          // Lowest Time Clear
                        lhr + "\n" + //2
                        lmin + "\n" + //3
                        (int)lsec + "\n" + //4
                                           // Highestest Time Clear
                        hhr + "\n" + //5
                        hmin + "\n" + //6
                        (int)hsec + "\n" + //7

                        moststageclear + "\n" + // Most Clear 8
                        lessstageclear + "\n" + // Less Clear 9
                        mostonegamekill + "\n" + //Most Kill in one game 10
                        mostgoblinkill + "\n" +//Most goblin Kill in one game 11
                        totalgoblinkill + "\n" + // Total goblin kill in all game 12
                        mostslimekill + "\n" +//Most slime Kill in one game 13
                        totalslimekill + "\n" + // Total slime kill in all game 14
                        totalminokill + "\n" + // Total Boss kill in all game 15
                        totalclear + "\n" + // Total game clear - must same number as Boss kill because kill boss mean clear game 16
                        totalplay; // Total play no matter clear or not 17
        if (File.Exists(path))
        {
            File.SetAttributes(path, FileAttributes.Normal);
            //File.Delete(path);
        }
        path = Application.dataPath + "/Save/" + username + ".txt";
        File.WriteAllText(path,content);
    }

    public static void readFile()
    {
        StreamReader reader = new StreamReader(path);
        while ((line = reader.ReadLine()) != null)
        {
            lines.Add(line);
        }
        reader.Close();
    }
    public static void readSettingFile()
    {
        StreamReader reader = new StreamReader(settingpath);
        while ((settingline = reader.ReadLine()) != null)
        {
            settinglines.Add(settingline);
        }
        reader.Close();
        volume = float.Parse(settinglines[0]);
    }

    public static void resetCatch()
    {
        lines = new List<string>();
    }
    public static void catchFile()
    {
        playerProfile = username;
        lhr = int.Parse(lines[2]);
        lmin = int.Parse(lines[3]);
        lsec = float.Parse(lines[4]);
        hhr = int.Parse(lines[5]);
        hmin = int.Parse(lines[6]);
        hsec = float.Parse(lines[7]);
        moststageclear = int.Parse(lines[8]);
        lessstageclear = int.Parse(lines[9]);
        mostonegamekill = int.Parse(lines[10]);
        mostgoblinkill = int.Parse(lines[11]);
        totalgoblinkill = int.Parse(lines[12]);
        mostslimekill = int.Parse(lines[13]);
        totalslimekill = int.Parse(lines[14]);
        totalminokill = int.Parse(lines[15]);
        totalclear = int.Parse(lines[16]);
        totalplay = int.Parse(lines[17]);
    }

    public static void EndGameUpadate()
    {
        hr = timecount.Hours; min = timecount.Minutes; sec = timecount.Seconds;
        //Highest-Lowest TimeCheck
        highestTime();
        //lowestTime();
        if (minokilled)
        {
            lowestTime();
        }

        //Highest-Lowest StageCheck
        if(stageCount > moststageclear)
        {
            moststageclear = stageCount;
        }
        else if(stageCount < lessstageclear || lessstageclear == 0)
        {
            lessstageclear = stageCount;
        }
        
        totalgoblinkill += goblinkill;
        totalslimekill += slimekill;

        if (goblinkill > mostgoblinkill)
        {
            mostgoblinkill = goblinkill;
        }
        if (slimekill > mostslimekill)
        {
            mostslimekill = slimekill;
        }
        if (onegamekill > mostonegamekill)
        {
            mostonegamekill = onegamekill;
        }



        writeFile();
    }

    public static void MainMenuReset()
    {
        hr = 0;
        min = 0;
        sec = 0;
        stageCount = 0;
        playerdead = false;
        minokilled = false;
        //PlayerMovement.animator.SetBool("Dead", false);
        //MinotaurAI.animator.SetBool("Dead", false);

        //Player Stat Reset
        hp = 100;
        atk = 0;
        def = 0;
        weapon = "Sword";
        weaponatk = 20;

        //Enemy Stat Reset
        goblinhp = 75;
        goblinatk = 10;
        goblindef = 0;
        slimehp = 50;
        slimeatk = 5;
        slimedef = 0;
        minohp = 500;
        minoatk = 20;
        minodef = 5;
    }

    public static void highestTime()
    {
        if (hr == hhr)
        {
            if (min == hmin)
            {
                if (sec > hsec)
                {
                    hsec = sec;
                }
            }
            else if (min > hmin)
            {
                hmin = min;
                hsec = sec;
            }
        }
        else if (hr > hhr){
            hhr = hr;
            hmin = min;
            hsec = sec;
        }
    }
    public static void lowestTime()
    {
        if (lhr == 0 && lmin == 0 && lsec == 0)
        {
            lhr = hr;
            lmin = min;
            lsec = sec;
        }
        else if (hr == lhr)
        {
            if (min == lmin)
            {
                if (sec < lsec)
                {
                    lsec = sec;
                }
            }
            else if (min < lmin)
            {
                lmin = min;
                lsec = sec;
            }
        }
        else if (hr < lhr)
        {
            lhr = hr;
            lmin = min;
            lsec = sec;
        }
    }
}
