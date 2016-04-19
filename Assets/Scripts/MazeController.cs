using UnityEngine;
using System.Collections;

public class MazeController : MonoBehaviour {

    public int width, height;
    private byte[,,] matrix;
    public GameObject cryptWall, land;
    private float cellScale, cellWidth, cellHeight;
    private GameObject[,] drawnCells;
    private GameObject[,,] drawnWalls;
    private GameObject player;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
        Renderer renderer = cryptWall.GetComponent<Renderer>();
        cellScale = Mathf.Min(Screen.width / (width + 4f), Screen.height / (height + 4f)) / (renderer.bounds.extents[0] * 2f); 
        cellWidth = renderer.bounds.extents[0] * cellScale * 2f;
        cellHeight = renderer.bounds.extents[1] * cellScale * 2f;
        player.transform.localScale = Vector3.one * cellScale * 2f;
        Debug.Log(Screen.width + " " + Screen.height);
        Debug.Log(Screen.currentResolution.width + " " + Screen.currentResolution.height);
        Debug.Log(cellScale + " " + cellWidth + " " + cellHeight);

        generateRandomMaze();
        drawWalls();
        drawCells();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void drawCells() {
        drawnCells = new GameObject[height + 2, width + 2];
        for (int i = 0; i < height + 2; ++i) {
            for (int j = 0; j < width + 2; j += width + 1) {
                GameObject wall = Instantiate(cryptWall, cryptWall.transform.position, cryptWall.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(cellScale, cellScale, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnCells[i, j] = wall;
            }
        }
        for (int i = 0; i < height + 2; i += height + 1) {
            for (int j = 0; j < width + 2; ++j) {
                GameObject wall = Instantiate(cryptWall, cryptWall.transform.position, cryptWall.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(cellScale, cellScale, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnCells[i, j] = wall;
            }
        }
        for (int i = 1; i < height + 1; ++i) {
            for (int j = 1; j < width + 1; ++j) {
                GameObject floor = Instantiate(land, land.transform.position, land.transform.rotation) as GameObject;
                floor.transform.SetParent(this.transform);
                floor.transform.localScale = new Vector3(cellScale, cellScale, 1f);
                floor.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight - cellHeight / 2f, 0f);
                drawnCells[i, j] = floor;
            }
        }
    }

    private void drawWalls() {
        drawnWalls = new GameObject[height, width, 2];
        for(int i = 1; i < height; ++i)
            for(int j = 1; j <= width; ++j) {
                if (matrix[i - 1, j - 1, 0] == 0)
                    continue;
                GameObject wall = Instantiate(cryptWall, cryptWall.transform.position, cryptWall.transform.rotation) as GameObject;
                wall.transform.SetParent(this.transform);
                wall.transform.localScale = new Vector3(cellScale, 1f, 1f);
                wall.transform.localPosition = new Vector3(j * cellWidth - (width / 2f) * cellWidth - cellWidth / 2f, i * cellHeight - (height / 2f) * cellHeight, 0f);
                drawnWalls[i - 1, j - 1, 0] = wall;
            }
        for(int i = 1; i <= height; ++i)
            for(int j = 1; j < width; ++j) {
                if (matrix[i - 1, j - 1, 1] == 0)
                    continue;
                GameObject wall = Instantiate(cryptWall, cryptWall.transform.position, cryptWall.transform.rotation) as GameObject;
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
