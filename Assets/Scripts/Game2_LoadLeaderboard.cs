using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game2_LoadLeaderboard : MonoBehaviour {
	void Start () {
		Text textArea = GameObject.Find("LeaderboardTextArea").GetComponent<Text>();
		textArea.text = Game2_Reader.getLeaderboardString();
	}
}
