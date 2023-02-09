using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private List<string> m_sliderKeys;
    [SerializeField]
    private List<Slider> m_sliders;

    private void Start()
    {
        for (int i = 0; i < m_sliders.Count; i++)
        {
            var slider = m_sliders[i];

            slider.onValueChanged.Invoke(slider.value);
            PlayerPrefs.SetFloat(m_sliderKeys[i], slider.value/100f);
        }
        PlayerPrefs.Save();
    }

    public void SliderMoved()
    {
        for (int i = 0; i < m_sliders.Count; i++)
        {
            var slider = m_sliders[i];
            PlayerPrefs.SetFloat(m_sliderKeys[i], slider.value / 100f);
        }
        PlayerPrefs.Save();
    }
}
