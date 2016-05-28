using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game2_MazeGenerator {
    private static Pair<int, int> right = new Pair<int, int>(0, 1);
    private static Pair<int, int> up = new Pair<int, int>(-1, 0);
    private static Pair<int, int> down = new Pair<int, int>(1, 0);
    private static List<Pair<int, int>> directions = new List<Pair<int, int>> { right, up, down };

    public static List<Pair<int, int>> generatePath(int dimension, System.Random rnd) {
        List<Pair<int, int>> path = new List<Pair<int, int>>();
        Pair<int, int> currentCell = new Pair<int, int>();

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

    private static int getDistance(Pair<int, int> p1, Pair<int, int> p2) {
        return (Math.Abs(p1.fst - p2.fst) + Math.Abs(p1.snd - p2.snd));
    }

    private static int getClosestPathIndex(List<Pair<int, int>> path, Pair<int, int> point) {
        int min = getDistance(path[0], point);
        int idx = 0;
        for (int i = 1; i < path.Count; ++i) {
            int val = Math.Min(min, getDistance(point, path[i]));
            if (min > val) {
                min = Math.Min(min, getDistance(point, path[i]));
                idx = i;
            }
        }
        return idx;
    }

    public static int[,] generateMaze(int dimension, List<Pair<int, int>> path) {
        System.Random rnd = new System.Random();
        int[,] maze = new int[dimension, dimension];

        for (int i = 0; i < path.Count; ++i)
            maze[path[i].fst, path[i].snd] = i;

        for (int i = 0; i < dimension; ++i)
            for (int j = 0; j < dimension; ++j) {
                Pair<int, int> point = new Pair<int, int>(i, j);
                if (!path.Contains(point)) {
                    int idx = getClosestPathIndex(path, point);
                    int value = maze[path[idx].fst, path[idx].snd];

                    if (idx <= path.Count / 2) {
                        maze[i, j] = rnd.Next(0, 2 * dimension) - dimension / 2;
                    }
                    else {
                        maze[i, j] = rnd.Next(0, (int)(dimension * 3.5)) - dimension;
                    }
                    maze[i, j] = (maze[i, j] < 0) ? -1 : maze[i, j];
                }
            }

        return maze;
    }

    public static bool directionChanged(Pair<int, int> p1, Pair<int, int> p2, Pair<int, int> p3) {
        if ((p2.fst - p1.fst == p3.fst - p2.fst) ||
            (p2.snd - p1.snd == p3.snd - p2.snd))
            return false;
        return true;
    }

	public static int evaluate(List<Pair<int, int>> path) {
        int value = 0;

        value += path.Count;
        for (int i = 2; i < path.Count; ++i)
            if (directionChanged(path[i - 2], path[i - 1], path[i]))
                value += 5;

        return value;
	}

    public static void generatePaths(int number, int dimension) {
        List<Pair<int, List<Pair<int, int>>>> paths = new List<Pair<int, List<Pair<int, int>>>>();
        System.Random rnd = new System.Random();
        int generateNumber = number * 100;
        for (int i = 0; i < generateNumber; ++i) {
            List<Pair<int, int>> path = Game2_MazeGenerator.generatePath(dimension, rnd);
            int value = evaluate(path);
            paths.Add(new Pair<int, List<Pair<int, int>>>(value, path));
        }

        paths.Sort((x, y) => y.fst.CompareTo(x.fst));

        Game2_Writer.refreshPathsFile(dimension);
        for (int i = 0; i < number; ++i) {
            Game2_Writer.appendPathsFile(paths[i].fst, paths[i].snd, dimension);
        }
    }
}
