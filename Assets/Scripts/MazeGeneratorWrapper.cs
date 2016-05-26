using UnityEngine;
using System.Collections;

public class MazeGeneratorWrapper : MonoBehaviour {

    public static int width, height;
    public int finalLine, finalColumn;
    private byte[,,] matrix;
    public GameObject wallPrefab, landPrefab, exitPrefab;
    private float cellScale, cellWidth, cellHeight;
    private GameObject[,] drawnCells;
    private GameObject[,,] drawnWalls;
    private GameObject player;
	private MazeGenerator mazeGenerator;

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

		mazeGenerator = new MazeGenerator (height, width);
		matrix = mazeGenerator.generateRandomMaze ();
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

    
}
