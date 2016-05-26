using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour {

    public int[] width = { 5, 10, 25 };
    public int[] height = { 5, 10, 25 };

    public void setGame1Difficulty(int difficulty)
    {
        MazeGeneratorWrapper.width = width[difficulty];
		MazeGeneratorWrapper.height = height[difficulty];
		GameOver.gameMode = difficulty;
    }
}
