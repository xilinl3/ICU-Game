using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Achievement : MonoBehaviour
{
    public float duration = 1.5f; // 渐隐持续时间（秒）
    public static Achievement instance;

    private float Chess;
    private GameObject prompts;
    private int ChessNumber;
    private CanvasGroup canvasGroup;

    private bool hasShownEatOne = false;
    private bool hasShownEatHalf = false;
    private bool hasShownEatLastOne = false;
    private bool hasShownEatAll = false;
    private bool hasShownDead = false;
    private bool hasShownVolumeChange = false;

    private GameObject BlackScreen;

    void Start()
    {
        ChessNumber = GameObject.FindWithTag("Player")?.GetComponent<player_behaviors>()?.totalCheese ?? 0;
        canvasGroup = GameObject.Find("EarCanvas")?.GetComponent<CanvasGroup>();
        BlackScreen = GameObject.Find("Canvas").transform.Find("BlackScreen")?.gameObject;
    }

    void Update()
    {
        Chess = GameObject.FindWithTag("Player")?.GetComponent<player_behaviors>()?.collectedCheese ?? 0;
        showAchievement();
    }

    private void showAchievement()
    {
        // 当奶酪数为1时显示成就
        if (Chess == 1 && !hasShownEatOne)
        {
            ShowPrompt("Achievement/EatOne");
            hasShownEatOne = true;
        }
        // 达到一半奶酪时显示成就
        else if (Chess == ChessNumber / 2 && !hasShownEatHalf)
        {
            ShowPrompt("Achievement/EatHalfofAll");
            hasShownEatHalf = true;
        }
        // 达到倒数第二个奶酪时显示成就
        else if (Chess == ChessNumber - 1 && !hasShownEatLastOne)
        {
            ShowPrompt("Achievement/EatLastOne");
            hasShownEatLastOne = true;
        }
        // 收集所有奶酪时显示成就
        else if (Chess == ChessNumber && !hasShownEatAll)
        {
            ShowPrompt("Achievement/EatAll");
            hasShownEatAll = true;
        }
        // 死亡时显示成就
        else if(BlackScreen != null && BlackScreen.activeSelf && !hasShownDead)
        {
            StartCoroutine(ShowDeathAchievementWithDelay());
            hasShownDead = true;
        }
        // 音量变化时显示成就
        else if (Volume.instance.volumeChanged &&!hasShownVolumeChange)
        {
            ShowPrompt("Achievement/Volume");
            hasShownVolumeChange = true;
        }
    }

    private IEnumerator ShowDeathAchievementWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        ShowPrompt("Achievement/Dead");
    }

    public void ShowPrompt(string path)
{
    // 查找指定路径的成就提示对象
    prompts = GameObject.Find("EarCanvas")?.transform.Find(path)?.gameObject;

    if (prompts == null)
    {
        Debug.LogError($"Failed to find prompt at path: EarCanvas/{path}");
        return;
    }

    if (canvasGroup == null)
    {
        Debug.LogError("CanvasGroup not found on the EarCanvas. Please check if it exists.");
        return;
    }

    // 激活成就提示对象并开始淡出效果
    prompts.SetActive(true);
    canvasGroup.alpha = 1; // 初始化透明度
    StartCoroutine(FadeOutAndDisableCanvas()); // 启动渐隐协程
}


    private IEnumerator FadeOutAndDisableCanvas()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1.0f - Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        if (prompts != null) prompts.SetActive(false);
    }
}
