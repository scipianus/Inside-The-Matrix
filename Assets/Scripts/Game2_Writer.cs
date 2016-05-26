using UnityEngine;
using System.Collections;
using System.IO;

public class Game2_Writer {
	private static string leaderboardFilePath = Application.persistentDataPath + "/leaderboard";
	private static string pathsFilePath = Application.persistentDataPath + "/paths";

	private static void refreshFile(string filePath) {
		if (!File.Exists(filePath))
			File.Create(filePath);
		FileStream fileStream = File.OpenWrite(filePath);
		fileStream.SetLength(0);
		fileStream.Close();
	}

	private static void appenToFile(string filePath, string text) {
		if (!File.Exists(filePath))
			File.Create(filePath);
		StreamWriter fileStream = File.AppendText(filePath);
		fileStream.WriteLine(text);
		fileStream.Close();
	}

	public static void refreshLeaderboardFile() {
		refreshFile(leaderboardFilePath);
	}

	public static void refreshPathsFile() {
		refreshFile(pathsFilePath);
	}

	public static void appendLeaderboardFile(string name, float score) {
		appenToFile(leaderboardFilePath, name + " " + score);
	}

	public static void appendPathsFile() {

	}
}
