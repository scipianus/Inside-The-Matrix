using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game2_PassToLeaderboard : MonoBehaviour {
    private static Text inputText;
    private static Text scoreText;
    public Text inputField;
    public Text scoreField;

    public void SaveToLeaderboard() {
        string name = inputField.text;
        if (name == "")
            name = "Unknown Player";
        float score = float.Parse(scoreField.text);
        Game2_Writer.appendLeaderboardFile(name, score);
    }
}
