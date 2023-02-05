using UnityEngine;

public class SFXSingleton : MonoBehaviour
{
	public static SFXSingleton Instance => s_instance;
	private static SFXSingleton s_instance = null;

	[SerializeField]
	public AudioSource AudioSource;

	[SerializeField] public AudioClip WaterClip1;
	[SerializeField] public AudioClip WaterClip2;
	[SerializeField] public AudioClip WaterClip3;

	private void Awake()
	{
		if (s_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		s_instance = this;
	}

	public void PlayWaterSFX()
	{
		int random = Random.Range(0, 3);
		switch (random)
		{
			case 0: AudioSource.PlayOneShot(WaterClip1); break;
			case 1: AudioSource.PlayOneShot(WaterClip2); break;
			case 2: AudioSource.PlayOneShot(WaterClip3); break;
		}
	}

	private void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}
}
