using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LoginFile : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public GameObject nonexistalert;
    public GameObject mainmenu;
    public Text incorrectAlert;
    public Text nullAlert;
    public Text createAlert;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        StatCatch.path = Application.dataPath + "/Save"; 
        
        if (!Directory.Exists(StatCatch.path))
        {
            Directory.CreateDirectory(StatCatch.path);
        }
        loadSetting();
        usernameField.Select();
        if (StatCatch.playerProfile != "" && StatCatch.playerProfile != null)
        {
            alreadyLogin();
        }
    }


    public void checkExist()
    {
        StatCatch.username = usernameField.text;
        StatCatch.password = passwordField.text;
        if (StatCatch.username == "" || StatCatch.username == null)
        {
            nullAlert.gameObject.SetActive(true);
            usernameField.Select();
            
        }
        else if (StatCatch.password == "" || StatCatch.password == null)
        {
            nullAlert.gameObject.SetActive(true);
            passwordField.Select();
        }
        else
        {
            nullAlert.gameObject.SetActive(false);
            //path = "Assets/Resources/Save/" + username + ".txt";
            StatCatch.path = Application.dataPath + "/Save/" + StatCatch.username + ".txt";

            if (!File.Exists(StatCatch.path))
            {
                //File.WriteAllText(path, "Login log \n\n"); //Replace all Text
                //File.WriteAllText(path, username + "\n" + password);
                nonexistalert.SetActive(true);
                createAlert.text = "This will create profile\n\""+ StatCatch.username +"\".";

            }
            else
            {
                StatCatch.readFile();
                checkPassword();
            }
        }
    }

    void checkPassword()
    {
        if (StatCatch.lines[1] == StatCatch.password)
        {
            loginSuccess();
        }
        else
        {
            incorrectAlert.gameObject.SetActive(true);
            StatCatch.resetCatch();
        }
    }

    void loginSuccess()
    {
        StatCatch.catchFile();


        incorrectAlert.gameObject.SetActive(false);
        usernameField.gameObject.SetActive(false);
        passwordField.gameObject.SetActive(false);
        mainmenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void alreadyLogin()
    {
        incorrectAlert.gameObject.SetActive(false);
        usernameField.gameObject.SetActive(false);
        passwordField.gameObject.SetActive(false);
        mainmenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void confirmCreate()
    {
        //path = Application.dataPath + "/Save/" + username + ".txt";
        StatCatch.writeFile();
        StatCatch.readFile();
        //File.WriteAllText(StatCatch.path, StatCatch.username + "\n" + StatCatch.password);
        nonexistalert.SetActive(false);
        loginSuccess();
    }
    public void cancleCreate()
    {
        incorrectAlert.gameObject.SetActive(false);
        usernameField.text = "";
        passwordField.text = "";
        StatCatch.username = "";
        StatCatch.password = "";
        nonexistalert.SetActive(false);
    }

    public void showPassword()
    {
        //Debug.Log("Show Password Clicked");
        //Debug.Log(passwordField.contentType == InputField.ContentType.Standard);
        if (passwordField.contentType == InputField.ContentType.Standard)
        {
            passwordField.contentType = InputField.ContentType.Password;
        }
        else if(passwordField.contentType == InputField.ContentType.Password)
        {
            passwordField.contentType = InputField.ContentType.Standard;
        }
        passwordField.Select();


    }

    public void loadSetting()
    {
        Debug.Log("Load Setting");
        StatCatch.settingpath = Application.dataPath + "/Settings";
        if (!Directory.Exists(StatCatch.settingpath))
        {
            Directory.CreateDirectory(StatCatch.settingpath);
        }
        if (Directory.Exists(StatCatch.settingpath))
        {
            StatCatch.settingpath = Application.dataPath + "/Settings/Setting.txt";
            if (!File.Exists(StatCatch.settingpath))
            {
                File.WriteAllText(StatCatch.settingpath, StatCatch.volume.ToString());
            }
            else
            {
                StatCatch.readSettingFile();
                mixer.SetFloat("Vol",StatCatch.volume);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
