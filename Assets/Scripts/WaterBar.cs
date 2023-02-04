using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
	[SerializeField]
	private Image m_fillBar;

	private WaterManager m_waterManager = null;

	private void Start()
	{
		m_waterManager = WaterManager.Instance;
	}

	private void Update()
	{
		m_fillBar.fillAmount = m_waterManager.CurrentWaterPercentage;
	}
}
