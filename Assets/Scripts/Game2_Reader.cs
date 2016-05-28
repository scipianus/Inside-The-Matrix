using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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

    public static List<Pair<int, int>> getRandomPath(int dimmension) {
        List<Pair<int, int>> path = new List<Pair<int, int>>();
        StreamReader fileStream = new StreamReader(pathsFilePath + dimmension);
        System.Random rnd = new System.Random();
        List<string> paths = File.ReadAllLines(pathsFilePath + dimmension).ToList();

        int idx = rnd.Next(0, paths.Count);
        string[] parsedLine = paths[idx].Split(' ');
        for (int i = 1; i < parsedLine.Length; i += 2)
            path.Add(new Pair<int, int>(int.Parse(parsedLine[i]), int.Parse(parsedLine[i + 1])));
        return path;
    }

    public static void checkLeaderboardExistance() {
        if (!File.Exists(leaderboardFilePath))
            Game2_Writer.refreshLeaderboardFile();
    }

    public static void checkPathsExistance(int dimension) {
        if (!File.Exists(pathsFilePath + dimension)) {
            Game2_Writer.refreshPathsFile(dimension);
            Game2_MazeGenerator.generatePaths(100, dimension);
        }
    }
}
