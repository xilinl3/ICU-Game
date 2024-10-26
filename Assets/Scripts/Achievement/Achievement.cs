using System.Collections;
using UnityEngine;

public class Achievement : MonoBehaviour
{
    private float Chess;
    private GameObject prompts;
    private int ChessNumber;
    private CanvasGroup canvasGroup;
    public float duration = 1.5f; // 渐隐持续时间

    private bool hasShownEatOne = false;
    private bool hasShownEatHalf = false;
    private bool hasShownEatLastOne = false;
    private bool hasShownEatAll = false;
    private bool hasShownDead = false;

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
    }

    private IEnumerator ShowDeathAchievementWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        ShowPrompt("Achievement/Dead");
    }

    private void ShowPrompt(string path)
    {
        // 通过路径找到对应成就提示
        prompts = GameObject.Find("EarCanvas")?.transform.Find(path)?.gameObject;
        if (prompts != null && canvasGroup != null)
        {
            prompts.SetActive(true);
            canvasGroup.alpha = 1; // 初始化透明度
            StartCoroutine(FadeOutAndDisableCanvas()); // 启动渐隐协程
        }
        else
        {
            Debug.LogError("Failed to find prompt or CanvasGroup.");
        }
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
