using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

	public AudioClip welcomeClip;
	public string welcomeText;

	public void playWelcome(AudioSource source)
	{
		source.PlayOneShot(welcomeClip);
	}
	
}
