using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {

	private AudioSource audioSource;
	private Slider slider;

	void Start()
	{
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> () as AudioSource;
		slider = this.GetComponent<Slider> () as Slider;
		slider.value = audioSource.volume;
	}

	void Update () {
		audioSource.volume = slider.value;
	}
}
