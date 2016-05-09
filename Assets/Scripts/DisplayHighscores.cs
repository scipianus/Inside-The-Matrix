using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour {

	public Text[,] highscoreFields = new Text[3, 3];
	Highscores highscoresManager;

	void Start() {
		for (int i = 0; i < 3; i ++) {
			for (int j = 0; j < 3; ++j) {
				highscoreFields [i, j] = GameObject.Find ("Score" + (i + 1) + "" +  (j + 1)).GetComponent<Text>();
				highscoreFields[i, j].text = j + 1 + ". Fetching...";
			}
		}


		highscoresManager = GetComponent<Highscores>();
		StartCoroutine("RefreshHighscores");
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList, int gameMode) {
		for (int j = 0; j < 3; ++j) {
			highscoreFields[gameMode, j].text = j + 1 + ". ";
			if (j < highscoreList.Length) {
				highscoreFields[gameMode, j].text += highscoreList[j].username + " - " + highscoreList[j].score;
			}
		}
	}

	IEnumerator RefreshHighscores() {
		while (true) {
			highscoresManager.DownloadHighscores();
			yield return new WaitForSeconds(30);
		}
	}
}
