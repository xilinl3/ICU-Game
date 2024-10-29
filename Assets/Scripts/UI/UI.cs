using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class UI : MonoBehaviour
{
    [SerializeField]public float amendment = 1f;

    [DllImport("User32.dll", EntryPoint = "keybd_event")]
    static extern void keybd_event(byte bvk, byte bScan, int dwFlags, int dwExtraInfo);

    //开始键
    public void Starting()
    {
        SceneManager.LoadScene("AwakeCg");
        Time.timeScale = 1;
    }

   //退出键
    public void ExitGame()
    {
        Volume.Instance.ResetVolumeChange(); // 重置音量变化标志
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    //继续键
    public void Continue()
    {
       GameObject.Find("Canvas").transform.Find("StopPage").gameObject.SetActive(false);
       amendment = 1f;
    }
    //主页
    public void HomePage()
    {
        SceneManager.LoadScene(0);
    }

    //重新开始
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    //打开设置页面
    public void SettingPage()
    {
        GameObject settingPage = GameObject.Find("Canvas").transform.Find("SettingPage").gameObject;
        if(settingPage != null)
        {
            settingPage.SetActive(true);
        }
        else
        {
            Debug.Log("SettingPage not found");
        }
    }
    //关闭设置页面
    public void CloseSettingPage()
    {
        GameObject settingPage = GameObject.Find("Canvas").transform.Find("SettingPage").gameObject;
        if(settingPage != null)
        {
            settingPage.SetActive(false);
        }
        else
        {
            Debug.Log("SettingPage not found");
        }
    }
    public void OpenHelpPage()
    {
        GameObject helpPage = GameObject.Find("Canvas").transform.Find("HelpPage").gameObject;
        if(helpPage != null)
        {
            helpPage.SetActive(true);
        }
        else
        {
            Debug.Log("HelpPage not found");
        }
    }
    public void CloseHelpPage()
    {
        GameObject helpPage = GameObject.Find("Canvas").transform.Find("HelpPage").gameObject;
        if(helpPage != null)
        {
            helpPage.SetActive(false);
        }
        else
        {
            Debug.Log("HelpPage not found");
        }
    }
    //暂停键
    public void StopGame()
    {
        GameObject pauseMenu = GameObject.Find("Canvas").transform.Find("StopPage").gameObject;
        amendment = 0f;
        pauseMenu.SetActive(true);
    }

    public void pressA()
    {
        keybd_event(65, 0, 1, 0);
    }

    public void pressSpace()
    {
        keybd_event(32, 0, 0, 0);
    }

    public void pressD()
    {
        Debug.Log("D has been pressed.");
        keybd_event(68, 0, 1, 0);
    }

    public void pressF()
    {
        keybd_event(70, 0, 0, 0);
    }

    public void pressR()
    {
        keybd_event(82, 0, 0, 0);
    }
}

