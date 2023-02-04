using UnityEngine;

public class MusicLoop : MonoBehaviour
{
	public AudioSource AudioSource;
	public AudioClip BGMIntro;

	// Start is called before the first frame update
	void Start()
	{
		AudioSource.PlayOneShot(BGMIntro);
		AudioSource.PlayScheduled(AudioSettings.dspTime + BGMIntro.length);
	}
}
