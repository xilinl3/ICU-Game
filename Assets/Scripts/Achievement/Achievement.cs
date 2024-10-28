using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievement : MonoBehaviour
{
    public float duration = 1.5f; // 渐隐持续时间（秒）
    public static Achievement instance;

    private float Chess;
    private GameObject prompts;
    private int ChessNumber;
    private CanvasGroup canvasGroup;
    private int currentMusicIndex;
    private int DeadNumber;
    public  AudioSource audioSource;

    private bool hasShownEatOne => PlayerPrefs.GetInt("hasShownEatOne", 0) == 1;
    private bool hasShownEatHalf => PlayerPrefs.GetInt("hasShownEatHalf", 0) == 1;
    private bool hasShownEatLastOne => PlayerPrefs.GetInt("hasShownEatLastOne", 0) == 1;
    private bool hasShownEatAll => PlayerPrefs.GetInt("hasShownEatAll", 0) == 1;
    private bool hasShownDead => PlayerPrefs.GetInt("hasShownDead", 0) == 1;
    private bool hasShownVolumeChange => PlayerPrefs.GetInt("hasShownVolumeChange", 0) == 1;
    private bool hasShownMusicChange => PlayerPrefs.GetInt("hasShownMusicChange", 0) == 1;
    private bool hasShownNoDead => PlayerPrefs.GetInt("hasShownNoDead", 0) == 1;
    private bool hasShownTime => PlayerPrefs.GetInt("hasShownTime", 0) == 1;

    private GameObject BlackScreen;
    private GameTime gameTime;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        ChessNumber = GameObject.FindWithTag("Player")?.GetComponent<player_behaviors>()?.totalCheese ?? 0;
        canvasGroup = GameObject.Find("EarCanvas")?.GetComponent<CanvasGroup>();
        BlackScreen = GameObject.Find("Canvas").transform.Find("BlackScreen")?.gameObject;
        currentMusicIndex = GameObject.Find("MusicRoom2/MusicChange").GetComponent<MusicChange>().currentMusicIndex;
        DeadNumber = GameObject.Find("Canvas").transform.Find("BlackScreen").GetComponent<black>().deadNumber;
        gameTime = GameObject.FindObjectOfType<GameTime>();
    }

    void Update()
    {
        Chess = GameObject.FindWithTag("Player")?.GetComponent<player_behaviors>()?.collectedCheese ?? 0;
        showAchievement();
    }

    private void showAchievement()
    {
        if (Chess == 1 && !hasShownEatOne)
        {
            ShowPrompt("Achievement/EatOne");
            PlayerPrefs.SetInt("hasShownEatOne", 1);
        }
        if (Chess == ChessNumber / 2 && !hasShownEatHalf)
        {
            ShowPrompt("Achievement/EatHalfofAll");
            PlayerPrefs.SetInt("hasShownEatHalf", 1);
        }
        if (Chess == ChessNumber - 1 && !hasShownEatLastOne)
        {
            ShowPrompt("Achievement/EatLastOne");
            PlayerPrefs.SetInt("hasShownEatLastOne", 1);
        }
        if (Chess == ChessNumber && !hasShownEatAll)
        {
            ShowPrompt("Achievement/EatAll");
            PlayerPrefs.SetInt("hasShownEatAll", 1);
        }
        if (BlackScreen != null && BlackScreen.activeSelf && !hasShownDead)
        {
            StartCoroutine(ShowDeathAchievementWithDelay());
            PlayerPrefs.SetInt("hasShownDead", 1);
        }
        if (currentMusicIndex != 0 && !hasShownMusicChange)
        {
            ShowPrompt("Achievement/Music");
            PlayerPrefs.SetInt("hasShownMusicChange", 1);
        }
        if (DeadNumber == 0 && !hasShownNoDead)
        {
            if (SceneManager.GetActiveScene().name == "EndingCg1" || SceneManager.GetActiveScene().name == "EndingCg2")
            {
                ShowPrompt("Achievement/NoDead");
                PlayerPrefs.SetInt("hasShownNoDead", 1);
            }
        }
        if (gameTime != null && gameTime.GetMinutes() >= 10 && !hasShownTime)
        {
            ShowPrompt("Achievement/Time");
            PlayerPrefs.SetInt("hasShownTime", 1);
        }
        if (Volume.Instance.volumeChanged && !hasShownVolumeChange)
        {
           ShowPrompt("Achievement/Volume");
            PlayerPrefs.SetInt("hasShownVolumeChange", 1);
        }
    }

    private IEnumerator ShowDeathAchievementWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        ShowPrompt("Achievement/Dead");
    }

    public void ShowPrompt(string path)
    {
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

        audioSource.Play();
        prompts.SetActive(true);
        canvasGroup.alpha = 1;
        StartCoroutine(FadeOutAndDisableCanvas());
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