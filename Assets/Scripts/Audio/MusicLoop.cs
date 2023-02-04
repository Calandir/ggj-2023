using UnityEngine;

public class MusicLoop : MonoBehaviour
{
	[SerializeField]
	public AudioSource AudioSource;

	[SerializeField]
	public AudioClip BGMIntro;

	// Start is called before the first frame update
	void Start()
	{
		AudioSource.PlayOneShot(BGMIntro);
		AudioSource.PlayScheduled(AudioSettings.dspTime + BGMIntro.length);
	}
}
