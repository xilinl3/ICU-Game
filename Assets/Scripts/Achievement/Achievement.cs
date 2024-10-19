using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public Achievement(string name, string message, int spriteIndex, GameObject achievementRef)
    {
        this.name = name;
        this.message = message;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
    }
    public bool EarnAchievement()
    {
        if(!unlocked)
        {
            unlocked = true;
            return true;
        }
        return false;
    }

}
