using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MazeExit : MonoBehaviour {

	private SceneChanger sceneChanger;

	void Start()
	{
		sceneChanger = GameObject.Find ("Scene Changer").GetComponent<SceneChanger> () as SceneChanger;
	}

    void OnTriggerEnter2D(Collider2D other) {
		if (other.transform.name == "Player") { // single player
			Timer timer = GameObject.Find ("Timer").GetComponent<Timer> ();
			GameOver.time = timer.time;
			sceneChanger.LoadScene ("GameOver");
		} else {
			if (other.transform.name == "Player1") { // multiplayer
				GameOver.gameMode = GameOver.gameMode + 3;
				GameOver.winner = 1;
				sceneChanger.LoadScene ("GameOver");
			}
			else 
				if (other.transform.name == "Player2") { // multiplayer
					GameOver.gameMode = GameOver.gameMode + 3;
					GameOver.winner = 2;
					sceneChanger.LoadScene ("GameOver");
				}
		}
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
			sceneChanger.LoadScene ("MainMenu");
        }
    }
}
