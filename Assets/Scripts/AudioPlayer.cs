using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

	private static AudioPlayer instance = null;
	public static AudioPlayer Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
	}
}
