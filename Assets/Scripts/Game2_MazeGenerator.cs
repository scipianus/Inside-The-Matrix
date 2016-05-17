using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2_MazeGenerator {
    private static Pair<int, int> right = new Pair<int, int>(0, 1);
    private static Pair<int, int> up = new Pair<int, int>(-1, 0);
    private static Pair<int, int> down = new Pair<int, int>(1, 0);
    private static List<Pair<int, int>> directions = new List<Pair<int, int>> { right, up, down };

    public static List<Pair<int, int>> generatePath(int dimension) {
        List<Pair<int, int>> path = new List<Pair<int, int>>();
        Pair<int, int> currentCell = new Pair<int, int>();
        System.Random rnd = new System.Random();

        currentCell.fst = rnd.Next(0, dimension);
        currentCell.snd = 0;
        path.Add(currentCell);

        if (currentCell.fst == 0)
            directions.Remove(up);
        else if (currentCell.fst == dimension - 1)
            directions.Remove(down);

        while (currentCell.snd != dimension - 1) {
            int next = rnd.Next(0, directions.Count);
            currentCell = new Pair<int, int>(currentCell.fst + directions[next].fst,
                                                currentCell.snd + directions[next].snd);
            path.Add(currentCell);

            if (directions[next] == right) {
                if (!directions.Contains(up) && currentCell.fst != 0
                    && !path.Contains(new Pair<int, int>(currentCell.fst - 1, currentCell.snd -1)))
                    directions.Add(up);
                if (!directions.Contains(down) && currentCell.fst != dimension - 1
                    && !path.Contains(new Pair<int, int>(currentCell.fst + 1, currentCell.snd - 1)))
                    directions.Add(down);
            }
            else if (directions[next] == up) {
                if (directions.Contains(down))
                    directions.Remove(down);
                if (currentCell.fst == 0)
                    directions.Remove(up);
            }
            else if (directions[next] == down) {
                if (directions.Contains(up))
                    directions.Remove(up);
                if (currentCell.fst == dimension - 1)
                    directions.Remove(down);
            }
        }

        return path;
    }

    public static int[,] generateMaze(int dimension, List<Pair<int, int>> path) {
        System.Random rnd = new System.Random();
        int[,] maze = new int[dimension, dimension];

        for (int i = 0; i < path.Count; ++i)
            maze[path[i].fst, path[i].snd] = i + rnd.Next(1, 4);

        for (int i = 0; i < dimension; ++i)
            for (int j = 0; j < dimension; ++j) {
                if (!path.Contains(new Pair<int, int>(i, j)))
                    maze[i, j] = rnd.Next(0, 2 * dimension);
            }

        return maze;
    }
}
