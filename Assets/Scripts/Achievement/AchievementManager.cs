using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPrefabs; // 成就预制体数组
    public Sprite[] sprites; // 成就图标数组
    public GameObject visualAchievement; // 成就图标数组
    public GameObject achievementMenu; // 成就菜单
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>(); // 成就字典
    public Sprite unLockedSprite; // 解锁图标 
    private static AchievementManager instance; // 成就管理器实例
    private int fadeTime = 2; // 淡出时间
     public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //Rember to Remove
        PlayerPrefs.DeleteAll();
        CreateAchievement("General","Press W","press W to unlock this achievement", 0,3);
        CreateAchievement("General","奶酪","其实老鼠不吃奶酪", 0,0);
        CreateAchievement("General","按钮","这什么按一下", 0,0);
        CreateAchievement("General","测试依赖","按下空格键", 0,0, new string[]{"Press W", "奶酪"});//完成W和奶酪后，直接激活这个成就
        

        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(false);
        }
        achievementMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            EarnAchievement("Press W");
        }

         if(Input.GetKeyDown(KeyCode.C))
        {
            EarnAchievement("奶酪");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EarnAchievement("测试依赖");
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            achievementMenu.SetActive(!achievementMenu.activeSelf);
        }
    }
    public void EarnAchievement(string title)
    {
        if(achievements[title].EarnAchievement())
        {
            //Do something awesome
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInFo("EarnCanvas", achievement, title);

            StartCoroutine(FadeAchievment(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }


    public void CreateAchievement(string parent, string title, string message, int spriteIndex, int progress, string[] dependencies = null)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefabs);
        

        Achievement newAchievement = new Achievement(title, message, spriteIndex, achievement, progress);

        achievements.Add(title, newAchievement);

        SetAchievementInFo(parent, achievement, title, progress);
        if(dependencies != null)
        {
            foreach(string achievmentTitle in dependencies)
            {
                Achievement dependency = achievements[achievmentTitle];
                dependency.Child = title;
                newAchievement.AddDependency(dependency);

                // Dependeny = press Space <-- Child = Press W
                // NewAchievment = press W -- > Press Space
            }
        }
    }

    public void SetAchievementInFo(string parent, GameObject achievement, string title, int progression =0)

    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1,1,1);

        string progress = progression >0?" " + PlayerPrefs.GetInt("Progression" + title) + "/" + progression.ToString(): string.Empty;

        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title + progress;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].Message;

        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }
    private IEnumerator FadeAchievment(GameObject achievment)
    {
        CanvasGroup canvasGroup = achievment.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;

        int startAlpha = 0;
        int endAlpha = 1;

        for(int i = 0; i < 2 ; i++)
        {
            float progress = 0.0f;
            while(progress < 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(2);

        startAlpha = 1;
        endAlpha = 0;
        }

        Destroy(achievment);
    }
}
