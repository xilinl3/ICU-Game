using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
   //开始键
   public void Starting()
    {
        SceneManager.LoadScene(1);
    }

   //退出键
    public void ExitGame()
    {
         Application.Quit();
    }

    //继续键
    public void Continue()
    {
        GameObject.Find("Canvas").transform.Find("StopPage").gameObject.SetActive(false);
        Time.timeScale = 1;  
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
}
