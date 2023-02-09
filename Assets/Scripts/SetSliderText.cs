using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;

    private void Awake()
    {
    }

    public void SetTextFromValue(float value)
    {
        m_text.text = $"{value}";
    }
}
