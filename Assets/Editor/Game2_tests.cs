using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class Game2_tests {

    [Test]
    public void ValidPath() {
        System.Random rnd = new System.Random();
        int dimension = rnd.Next(5, 18);
        List<Pair<int, int>> path = Game2_MazeGenerator.generatePath(dimension, rnd);
        Assert.IsNotNull(path);
        for (int i = 0; i < path.Count; ++i) {
            Assert.That(0 <= path[i].fst && path[i].fst < dimension);
            Assert.That(0 <= path[i].snd && path[i].snd < dimension);
            if (i > 0) {
                Assert.That(Math.Abs(path[i].fst - path[i - 1].fst) <= 1);
                Assert.That(Math.Abs(path[i].snd- path[i - 1].snd) <= 1);
            }
        }
    }

    [Test]
    public void MazeHasSolution() {
        System.Random rnd = new System.Random();
        int dimension = rnd.Next(5, 18);
        List<Pair<int, int>> path = Game2_MazeGenerator.generatePath(dimension, rnd);
        int[,] maze = Game2_MazeGenerator.generateMaze(dimension, path);
        bool[,] visited = new bool[dimension, dimension];
        Queue<Pair<int, int>> q = new Queue<Pair<int, int>>();
        Pair<int, int> start = new Pair<int, int>(path[0].fst, path[0].snd);
        Pair<int, int> end = new Pair<int, int>(path[path.Count - 1].fst, path[path.Count - 1].snd);
        int[] dirX = { -1, 0, 1, 0 };
        int[] dirY = { 0, 1, 0, -1 };

        for (int i = 0; i < dimension; ++i)
            for (int j = 0; j < dimension; ++j)
                visited[i, j] = false;

        q.Enqueue(start);
        while (q.Count > 0) {
            Pair<int, int> curr = q.Dequeue();
            for (int i = 0; i < 4; ++i) {
                Pair<int, int> next = new Pair<int, int>(curr.fst + dirX[i], curr.snd + dirY[i]);
                if((0 <= next.fst && next.fst < dimension) &&
                    (0 <= next.snd && next.snd < dimension) &&
                    (!visited[next.fst, next.snd]) &&
                    (maze[curr.fst, curr.snd] < maze[next.fst, next.snd])) {
                    visited[next.fst, next.snd] = true;
                    q.Enqueue(next);
                }
            }
        }
        Assert.That(visited[end.fst, end.snd] == true);
    }
}
