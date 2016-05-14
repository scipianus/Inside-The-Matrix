using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private AudioSource audioSource;

	void Start()
	{
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> () as AudioSource;
	}

    public void LoadScene(string sceneName)
    {
		if (sceneName == "Game1")
			audioSource.Pause ();
		else if (!audioSource.isPlaying)
			audioSource.Play ();
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
