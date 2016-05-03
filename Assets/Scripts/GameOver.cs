using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public static int gameMode;
    public static string time;
    private Text label;
    private const string gameOver = "Game over\n";
    
	// Use this for initialization
	void Start () {
        label = GameObject.Find("GameOverText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        switch(gameMode) {
            case 1:
                label.text = gameOver + "Your time was " + time;
                break;
            default:
                label.text = gameOver;
                break;
        }
	}
}
