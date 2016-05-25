using System.Collections;
using UnityEngine;

public class MazeGenerator
{
	private int height, width;
	private byte[,,] matrix;

	public MazeGenerator (int height, int width)
	{
		this.height = height;
		this.width = width;
	}

	public byte[,,] generateRandomMaze() {
		matrix = new byte[height, width, 2];
		for (int i = 0; i < height; ++i)
			for (int j = 0; j < width; ++j)
				matrix[i, j, 0] = matrix[i, j, 1] = 0;
		generateRandomMazeRecursive(0, 0, height - 1, width - 1);
		return matrix;
	}

	private void generateRandomMazeRecursive(int firstLine, int firstColumn, int lastLine, int lastColumn) {
		if (firstLine >= lastLine || firstColumn >= lastColumn)
			return;
		int pivotLine = Random.Range(firstLine, lastLine - 1);
		int pivotColumn = Random.Range(firstColumn, lastColumn - 1);
		for (int i = firstLine; i <= lastLine; ++i)
			matrix[i, pivotColumn, 1] = 1;
		for (int j = firstColumn; j <= lastColumn; ++j)
			matrix[pivotLine, j, 0] = 1;
		ArrayList holes = new ArrayList();
		holes.Add(new Vector3(Random.Range(firstLine, pivotLine), pivotColumn, 1));
		holes.Add(new Vector3(Random.Range(pivotLine + 1, lastLine), pivotColumn, 1));
		holes.Add(new Vector3(pivotLine, Random.Range(firstColumn, pivotColumn), 0));
		holes.Add(new Vector3(pivotLine, Random.Range(pivotColumn + 1, lastColumn), 0));
		int removeIndex = Random.Range(0, 3);
		for (int i = 0; i < 4; ++i)
			if (i != removeIndex) {
				Vector3 position = (Vector3)holes[i];
				matrix[(int)position[0], (int)position[1], (int)position[2]] = 0;
			}
		generateRandomMazeRecursive(firstLine, firstColumn, pivotLine, pivotColumn);
		generateRandomMazeRecursive(firstLine, pivotColumn + 1, pivotLine, lastColumn);
		generateRandomMazeRecursive(pivotLine + 1, firstColumn, lastLine, pivotColumn);
		generateRandomMazeRecursive(pivotLine + 1, pivotColumn + 1, lastLine, lastColumn);
	}
}

