using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public static int gameMode;
    public static float time;
	private string timeString;
    private Text label;
    private const string gameOver = "Game over\n";
    
	// Use this for initialization
	void Start () {
        label = GameObject.Find("GameOverText").GetComponent<Text>();

		if (gameMode >= 0 && gameMode < 3) {
			float minutes = time / 60;
			float seconds = time % 60;
			label.text = gameOver + "Your time was " + string.Format("{0:00}:{1:00}", minutes, seconds);
			Highscores.AddNewHighscore (Highscores.BIG_VALUE - Mathf.RoundToInt(time), GameOver.gameMode);
		}
		else
            label.text = gameOver;
        
    }
}
