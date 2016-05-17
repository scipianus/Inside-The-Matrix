using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameGridLogic : MonoBehaviour {
    public static int gridDimension = 6;
    private int[,] cellNumber;
    private Cell[,] cells;
    private Pair<int, int> currentCell;

    public int currentStep = 0;

	void Start () {
        cells = new Cell[gridDimension, gridDimension];

        for (int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j)
                cells[i, j] = new Cell(this, i, j);

        generateCells();
    }

    public bool isMoveLegal(Pair<int, int> cell) {
        return (0 <= cell.fst && cell.fst < gridDimension &&
                0 <= cell.snd && cell.snd < gridDimension &&
                cellNumber[cell.fst, cell.snd] > 1);
    }

    public void generateCells() {
        System.Random rnd = new System.Random();

        List<Pair<int, int>> path = Game2_MazeGenerator.generatePath(gridDimension);
        cellNumber = Game2_MazeGenerator.generateMaze(gridDimension, path);
        currentCell = path[0];

        for(int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                cells[i, j].setText(cellNumber[i, j].ToString());
                if (path.Contains(new Pair<int, int>(i, j)))
                    cells[i, j].setColor(Color.cyan);
            }
    }

    public void updateCells(Pair<int, int> oldCell, Pair<int, int> newCell) {
        cells[oldCell.fst, oldCell.snd].setColor(Color.red);
        cells[newCell.fst, newCell.snd].setColor(Color.green);

        for (int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                if (cellNumber[i, j] > 0) {
                    --cellNumber[i, j];
                    cells[i, j].setText(cellNumber[i, j].ToString());
                }
            }
    }

    public Pair<int, int> getCurrentCell() {
        return currentCell;
    }

    public void setCurrentCell(Pair<int, int> currentCell) {
        this.currentCell = currentCell;
    }

    public int getCurrentStep() {
        return currentStep;
    }

    public void setCurrentStep(int currentStep) {
        this.currentStep = currentStep;
    }
	
	void Update () {

    }
}
