using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class LightIntensitySlider : MonoBehaviour
{
    public Slider intensitySlider; // 滑条
    public Light2D light2D; // 目标Light2D

    private void Start()
    {
        // 从PlayerPrefs恢复强度，如果没有保存值，则默认设为1
        float savedIntensity = PlayerPrefs.GetFloat("lightIntensity", 1f);
        light2D.intensity = savedIntensity;

        // 初始化滑条值为恢复的强度
        intensitySlider.value = savedIntensity;

        // 添加滑条值变化的监听器
        intensitySlider.onValueChanged.AddListener(SetIntensity);
    }

    // 设置Light2D的强度，并将值保存到PlayerPrefs中
    private void SetIntensity(float value)
    {
        light2D.intensity = value;
        PlayerPrefs.SetFloat("lightIntensity", value);
        PlayerPrefs.Save(); // 立即保存
    }
}
