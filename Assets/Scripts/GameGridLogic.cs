using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameGridLogic : MonoBehaviour {
    public static int gridDimension = 5;
    private int[,] cellNumber;
    private int[,] initialCellNumber;
    private Cell[,] cells;
    private Pair<int, int> currentCell;
    private Pair<int, int> endCell;
    private List<Pair<int, int>> path;
    private float time;

    public GameObject GameOverScreen;


    public int currentStep = 0;

	void Start() {
        cells = new Cell[gridDimension, gridDimension];

        for (int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j)
                cells[i, j] = new Cell(this, i, j);

        Game2_Reader.checkLeaderboardExistance();
        Game2_Reader.checkPathsExistance(gridDimension);
        generateCells();
    }

    public bool isMoveLegal(Pair<int, int> cell) {
        return (0 <= cell.fst && cell.fst < gridDimension &&
                0 <= cell.snd && cell.snd < gridDimension &&
                cellNumber[cell.fst, cell.snd] >= 1);
    }

    public void generateCells() {
        System.Random rnd = new System.Random();
        path = Game2_Reader.getRandomPath(gridDimension);
        cellNumber = Game2_MazeGenerator.generateMaze(gridDimension, path);
        initialCellNumber = new int[gridDimension, gridDimension];

        //Preserver the initial cell numbers in another array
        for(int i = 0; i < gridDimension; i++)
            for(int j = 0; j < gridDimension; j++)
                initialCellNumber[i, j] = cellNumber[i, j];    

        currentCell = path[0];
        endCell = path[path.Count - 1];
        for(int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j)
                cells[i, j].setText(cellNumber[i, j].ToString());

        cells[currentCell.fst, currentCell.snd].setColor(Color.green);
        cells[endCell.fst, endCell.snd].setColor(Color.yellow);
    }

    public void endGame() {
        time = Time.timeSinceLevelLoad;
        GameOverVisibility gameScreen =  GameOverScreen.GetComponent<GameOverVisibility>();
        // Take into consideration the size of the grid when computing the penalty for extra steps
        float stepScaler = currentStep - path.Count + 20;
        float timeScaler = ((15 - time) > 0) ? 15 - time : 0; 
        float score = timeScaler + stepScaler;
        gameScreen.becomeVisible(score);
		Game2_Writer.appendLeaderboardFile("Catalin", score);
    }

    public void updateCells(Pair<int, int> oldCell, Pair<int, int> newCell) {
        cells[oldCell.fst, oldCell.snd].setColor(Color.red);
        cells[newCell.fst, newCell.snd].setColor(Color.green);

        for (int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                if (cellNumber[i, j] >= 0) {
                    --cellNumber[i, j];
                    cells[i, j].setText(cellNumber[i, j].ToString());
                    //if (path.Contains(new Pair<int, int>(i, j)))
                    //    cells[i, j].setColor(Color.cyan);
                }
            }

        if (newCell == endCell)
            endGame();
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
	
    public void Retry() {
        for (int i = 0; i < gridDimension; i++)
            for (int j = 0; j < gridDimension; j++)
                cellNumber[i, j] = initialCellNumber[i, j];

        currentStep = 0;
        currentCell = path[0];
        for(int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                cells[i, j].setText(cellNumber[i, j].ToString());
                if(cellNumber[i,j] >= 0)
                    cells[i, j].setColor(Color.red);
            }

        cells[currentCell.fst, currentCell.snd].setColor(Color.green);
        cells[endCell.fst, endCell.snd].setColor(Color.yellow);
    }

	void Update () {

    }
}
