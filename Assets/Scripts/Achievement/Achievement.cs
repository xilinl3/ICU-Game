using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private string message;
    public string Message
    {
        get { return message; }
        set { message = value; }
    }
    private bool unlocked;
    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }
    private int spriteIndex;
    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }
    private GameObject achievementRef;
    private List<Achievement> dependencies = new List<Achievement>();
    private string child;
    public string Child
    {
        get { return child; }
        set { child = value; }
    }

    private int currentProgression;
    private int maxProgression;
    
    public Achievement(string name, string message, int spriteIndex, GameObject achievementRef, int maxProgression)
    {
        this.name = name;
        this.message = message;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        this.maxProgression = maxProgression;

        LoadAchievement();
    }
    public void AddDependency(Achievement dependency)
    {
        dependencies.Add(dependency);
    }
    public bool EarnAchievement()
    {
        if(!unlocked && !dependencies.Exists(x => x.unlocked == false) && CheckProgress())
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unLockedSprite;
            SaveAchievement(true);

            if(child != null)
            {
                AchievementManager.Instance.EarnAchievement(child);
            }

            return true;
        }
        return false;
    }

    public void SaveAchievement(bool value)
    {
        unlocked = value;

        PlayerPrefs.SetInt("Progression" + name, currentProgression);
        PlayerPrefs.SetInt(name, value ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;
        if(unlocked)
        {
            currentProgression = PlayerPrefs.GetInt("Progression" + name);
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unLockedSprite;
        }
    }

    public bool CheckProgress()
    {
        currentProgression++;

        achievementRef.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Name + " " + currentProgression + "/" + maxProgression;

        SaveAchievement(false);

        if(maxProgression == 0)
        {
            return true;
        }
        if(currentProgression >= maxProgression)
        {
            return true;
        }

        return false;
    }
}
