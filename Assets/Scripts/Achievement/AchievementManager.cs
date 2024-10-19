using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPrefabs; // 成就预制体数组
    public Sprite[] sprites; // 成就图标数组
    public GameObject visualAchievement; // 成就图标数组
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>(); // 成就字典

    // Start is called before the first frame update
    void Start()
    {

        CreateAchievement("General","Press W","press W to unlock this achievement", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievement("Press W");
        }

    }
    public void EarnAchievement(string title)
    {
        if(achievements[title].EarnAchievement())
        {
            //Do something awesome
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInFo("EarnCanvas", achievement, title);

            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        //Destroy(achievement);
    }


    public void CreateAchievement(string parent, string title, string message, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefabs);

        Achievement newAchievement = new Achievement(title, message, spriteIndex, achievement);

        achievements.Add(title, newAchievement);

        SetAchievementInFo(parent, achievement, title);
    }

    public void SetAchievementInFo(string parent, GameObject achievement, string title)

    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1,1,1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].Message;

        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];

    }

}
