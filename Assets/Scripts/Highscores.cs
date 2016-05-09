﻿using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour {

	readonly string[] privateCode = {
		"Su0HxWaPKUqNP2MoGivO_wFDaKSl6TS0-4Bi8ORO4dOA",
		"sCRW72EgvUidrBtKDGqH7AVEsr9C5vr0C_vdh3L8Ix7g",
		"D_fSTW-ixEuBDQXqW6Tp5AdarhY5xBcEmWqjtFyo1Uxg"
	};
	readonly string[] publicCode = {
		"5730c9e26e51b60090ae1dd3",
		"5730d6ce6e51b900904062de",
		"5730d7196e51b900904063d8"
		};
	const string webURL = "http://dreamlo.com/lb/";

	DisplayHighscores highscoreDisplay;
	public Highscore[] highscoresList;
	static Highscores instance;

	void Start() {
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				AddNewHighscore (((i + j + 2) * j * (i + 9) + (j + 1) * (i + j + 7)) % 100 + 10, i);
			}
		}
	}

	void Awake() {
		highscoreDisplay = GetComponent<DisplayHighscores> ();
		instance = this;
	}

	public static void AddNewHighscore(int score, int gameMode) {
		instance.StartCoroutine(instance.UploadNewHighscore(score, gameMode));
	}

	IEnumerator UploadNewHighscore(int score, int gameMode) {
		string username = score.ToString();
		WWW www = new WWW(webURL + privateCode[gameMode] + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error)) {
			print ("Upload Successful");
			DownloadHighscores();
		}
		else {
			print ("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores() {
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase() {
		for (int i = 0; i < 3; ++i) {
			WWW www = new WWW (webURL + publicCode[i] + "/pipe/");
			yield return www;

			if (string.IsNullOrEmpty (www.error)) {
				FormatHighscores (www.text);
				highscoreDisplay.OnHighscoresDownloaded (highscoresList, i);
			} else {
				print ("Error Downloading: " + www.error);
			}
		}
	}

	void FormatHighscores(string textStream) {
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i ++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username,score);
		}
	}

}

public struct Highscore {
	public string username;
	public int score;

	public Highscore(string _username, int _score) {
		username = _username;
		score = _score;
	}

}