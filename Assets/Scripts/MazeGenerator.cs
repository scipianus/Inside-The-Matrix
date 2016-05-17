using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

    public static int width, height;
    public int finalLine, finalColumn;
    private byte[,,] matrix;
    public GameObject wallPrefab, landPrefab, exitPrefab;
    private float cellScale, cellWidth, cellHeight;
    private GameObject[,] drawnCells;
    private GameObject[,,] drawnWalls;
    private GameObject player;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
        Renderer renderer = wallPrefab.GetComponent<Renderer>();
        //cellScale = Mathf.Min(Screen.width / (width + 4f), Screen.height / (height + 4f)) / (renderer.bounds.extents[0] * 2f);
        cellScale = 10f; 
        cellWidth = renderer.bounds.extents[0] * cellScale * 2f;
        cellHeight = renderer.bounds.extents[1] * cellScale * 2f;
		if (player != null) // single player
        	player.transform.localScale = Vector3.one * cellScale * 2f;
		else { // multiplayer
			for (int i = 1; i <= 2; ++i) {
				player = GameObject.Find ("Player" + i);
				player.transform.localScale = Vector3.one * cellScale * 2f;
			}
		}
        //Debug.Log(Screen.width + " " + Screen.height);
        //Debug.Log(Screen.currentResolution.width + " " + Screen.currentResolution.height);
        //Debug.Log(cellScale + " " + cellWidth + " " + cellHeight);

        generateRandomMaze();
        drawWalls();
        drawCells();
    }

    private void drawCells() {
        drawnCells = new GameObject[height + 2, width + 2];
        int[] positions = new int[2 * (height + width)];
        int cnt = 0;
        for (int i = 0; i < height + 2; ++i) {
            for (int j = 0; j < width + 2; j += width + 1) {
                GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, wallPrefab.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(cellScale, cellScale, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnCells[i, j] = wall;
                if(i != 0 && i != height + 1)
                    positions[cnt++] = i * 10000 + j;
            }
        }
        for (int i = 0; i < height + 2; i += height + 1) {
            for (int j = 0; j < width + 2; ++j) {
                GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, wallPrefab.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(cellScale, cellScale, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnCells[i, j] = wall;
                if(j != 0 && j != width + 1)
                    positions[cnt++] = i * 10000 + j;
            }
        }
        for (int i = 1; i < height + 1; ++i) {
            for (int j = 1; j < width + 1; ++j) {
                GameObject floor = Instantiate(landPrefab, landPrefab.transform.position, landPrefab.transform.rotation) as GameObject;
                floor.transform.SetParent(this.transform);
                floor.transform.localScale = new Vector3(cellScale, cellScale, 1f);
                floor.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnCells[i, j] = floor;
            }
        }

        int pos = positions[Random.Range(0, cnt - 1)];
        finalLine = pos / 10000;
        finalColumn = pos % 10000;
        //Debug.Log("Iesirea este la " + finalLine + " , " + finalColumn);

        Renderer renderer = drawnCells[finalLine, finalColumn].GetComponent<Renderer>();
        renderer.enabled = false;
        BoxCollider2D collider = drawnCells[finalLine, finalColumn].GetComponent<BoxCollider2D>();
        collider.enabled = false;

        GameObject escapeFloor = Instantiate(exitPrefab, exitPrefab.transform.position, exitPrefab.transform.rotation) as GameObject;
        escapeFloor.transform.SetParent(this.transform);
        escapeFloor.transform.localScale = new Vector3(cellScale, cellScale, 1f);
        escapeFloor.transform.localPosition = new Vector3(finalColumn * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, finalLine * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
        drawnCells[finalLine, finalColumn] = escapeFloor;
    }

    private void drawWalls() {
        drawnWalls = new GameObject[height, width, 2];
        for(int i = 1; i < height; ++i)
            for(int j = 1; j <= width; ++j) {
                if (matrix[i - 1, j - 1, 0] == 0)
                    continue;
                GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, wallPrefab.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(cellScale, 1f, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight, 0f);
                drawnWalls[i - 1, j - 1, 0] = wall;
            }
        for(int i = 1; i <= height; ++i)
            for(int j = 1; j < width; ++j) {
                if (matrix[i - 1, j - 1, 1] == 0)
                    continue;
                GameObject wall = Instantiate(wallPrefab, wallPrefab.transform.position, wallPrefab.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(1f, cellScale, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnWalls[i - 1, j - 1, 1] = wall;
            }
    }

    private void generateRandomMaze() {
        matrix = new byte[height, width, 2];
        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                matrix[i, j, 0] = matrix[i, j, 1] = 0;
        generateRandomMazeRecursive(0, 0, height - 1, width - 1);
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
