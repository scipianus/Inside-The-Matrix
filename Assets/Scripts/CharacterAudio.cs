using UnityEngine;
using System.Collections;

public class CharacterAudio : MonoBehaviour {

	void Start () {
		AudioSource audioSource = this.GetComponent<AudioSource> () as AudioSource;
		AudioSource mainMenuAudioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> () as AudioSource;
		audioSource.volume = mainMenuAudioSource.volume;
	}

}
