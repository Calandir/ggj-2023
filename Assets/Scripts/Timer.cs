using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	[SerializeField]
	private Text m_text;

	private void Update()
	{
		if (MiscUtils.IsGameOver)
		{
			return;
		}

		float time = Time.time;
		float timeOneSignificantDigit = (float)(Math.Truncate(time * 10) / 10.0f);

		string timeFormatted = string.Format("{0:0.0}", timeOneSignificantDigit);
		m_text.text = timeFormatted;
	}
}
