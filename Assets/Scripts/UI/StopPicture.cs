using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopPicture : MonoBehaviour
{
    public Sprite stop;
    public Sprite play;
    public GameObject stoppage;
    private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stoppage.activeSelf)
        {
            buttonImage.sprite = play; // 如果stoppage打开，显示play图片
        }
        else
        {
            buttonImage.sprite = stop; // 如果stoppage关闭，显示stop图片
        }
    }
}
