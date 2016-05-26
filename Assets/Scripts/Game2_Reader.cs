using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Game2_Reader {
	private static string leaderboardFilePath = Application.persistentDataPath + "/leaderboard";
	private static string pathsFilePath = Application.persistentDataPath + "/paths";

	public static List<Pair<string, float>> getLeaderboardList() {
		List<Pair<string, float>> leaderboard = new List<Pair<string, float>>();
		StreamReader fileStream = new StreamReader(leaderboardFilePath);
		string line;

		while ((line = fileStream.ReadLine()) != null) {
			string[] parsedLine = line.Split(' ');
			leaderboard.Add(new Pair<string, float>(parsedLine[0], float.Parse(parsedLine[1])));
		}

		fileStream.Close();
		return leaderboard;
	}

	public static string getLeaderboardString() {
		StreamReader fileStream = new StreamReader(leaderboardFilePath);
		return fileStream.ReadToEnd();
	}
}
