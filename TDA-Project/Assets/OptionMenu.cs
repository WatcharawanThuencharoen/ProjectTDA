using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.IO;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    private string line = "";
    private List<string> lines = new List<string>();
    public AudioMixer mixer;
    public Slider slider;
    //Start is called before the first frame update
    void Start()
    {
        StatCatch.settingpath = Application.dataPath + "/Settings/Setting.txt";
        if (File.Exists(StatCatch.settingpath))
        {
            StreamReader reader = new StreamReader(StatCatch.settingpath);
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            reader.Close();
            StatCatch.volume = float.Parse(lines[0]);
        }
        setVol(StatCatch.volume);
        slider.value = StatCatch.volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setVol(float volume)
    {
        StatCatch.volume = volume;
        mixer.SetFloat("Vol", volume);

    }
    public void saveSetting()
    {
        if (File.Exists(StatCatch.settingpath))
        {
            File.SetAttributes(StatCatch.settingpath, FileAttributes.Normal);
            //File.Delete(path);
        }
        File.WriteAllText(StatCatch.settingpath, StatCatch.volume.ToString());
    }
}
