using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;

public class MyTests {

	[Test]
	public void ValidMazeMatrixValues()
	{
		int height = Random.Range (5, 25);
		int width = Random.Range (5, 25);
		MazeGenerator mazeGenerator = new MazeGenerator (height, width);
		byte[,,] matrix = mazeGenerator.generateRandomMaze ();
		for (int i = 0; i < height; ++i)
			for (int j = 0; j < width; ++j)
				for (int k = 0; k < 2; ++k)
					Assert.IsNotNull (matrix [i, j, k]);
	}

	[Test]
	public void ValidMazeAccessibility()
	{
		int height = Random.Range (5, 25);
		int width = Random.Range (5, 25);
		MazeGenerator mazeGenerator = new MazeGenerator (height, width);
		byte[,,] matrix = mazeGenerator.generateRandomMaze ();

		const int bigVal = 1000;
		int[] dx = { 0, 1, 0, -1 };
		int[] dy = { 1, 0, -1, 0 };
		bool[,] visited = new bool[height, width];
		Queue q = new Queue ();
	
		for (int i = 0; i < height; ++i)
			for (int j = 0; j < width; ++j)
				visited [i, j] = false;
		visited [0, 0] = true;
		q.Enqueue (0 * bigVal + 0);

		while (q.Count > 0) {
			int hashedPosition = (int)q.Dequeue ();
			int line = hashedPosition / bigVal;
			int column = hashedPosition % bigVal;
			for (int k = 0; k < 4; ++k) {
				if (k == 0 && matrix [line, column, 1] == 1) // wall
					continue;
				if (k == 1 && matrix[line,column, 0] == 1) // wall
					continue;
				if (k == 2 && column > 0 && matrix [line, column - 1, 1] == 1) // wall
					continue;
				if (k == 3 && line > 0 && matrix[line - 1, column, 0] == 1) // wall
					continue;
				int newLine = line + dx [k];
				int newColumn = column + dy [k];
				if (newLine >= 0 && newLine < height && newColumn >= 0 && newColumn < width) {
					if (!visited [newLine, newColumn]) {
						visited [newLine, newColumn] = true;
						q.Enqueue (newLine * bigVal + newColumn);
					}
				}
			}
		}
		for (int i = 0; i < height; ++i)
			for (int j = 0; j < width; ++j)
				Assert.That (visited [i, j] == true);
	}
}
