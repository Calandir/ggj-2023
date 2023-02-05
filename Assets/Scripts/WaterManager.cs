using UnityEngine;

public class WaterManager : MonoBehaviour
{
	public static WaterManager Instance => s_instance;
	private static WaterManager s_instance = null;

	public float CurrentWaterPercentage => m_currentWater / m_maxWater;
	private float m_currentWater = 0.0f;

	public float WaterLossPerSplit => m_waterLossPerSplit;

	[SerializeField]
	private float m_startWater = 20.0f;

	[SerializeField]
	private float m_maxWater = 20.0f;

	[SerializeField]
	private float m_waterLossPerSecond = 1.0f;

	[SerializeField]
	private float m_waterLossPerSplit = 1.0f;

	private void Awake()
	{
		if (!ValidateInstance())
		{
			return;
		}

		m_currentWater = Mathf.Min(m_startWater, m_maxWater);
	}

	public void AwardWater(float amount)
	{
		m_currentWater += amount;

		if (SFXSingleton.Instance != null)
		{
			SFXSingleton.Instance.PlayWaterSFX();
		}
	}

	public void SpendWater(float amount)
	{
		m_currentWater -= amount;
	}

	private void Update()
	{
		m_currentWater -= m_waterLossPerSecond * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.LeftBracket))
		{
			SpendWater(2.0f);
		}
		if (Input.GetKeyDown(KeyCode.RightBracket))
		{
			AwardWater(2.0f);
		}

		m_currentWater = Mathf.Clamp(m_currentWater, 0.0f, m_maxWater);
	}

	private bool ValidateInstance()
	{
		if (s_instance != null)
		{
			Destroy(gameObject);
			return false;
		}

		s_instance = this;
		return true;
	}

	private void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}
}
