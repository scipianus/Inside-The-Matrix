using UnityEngine;
using System.Collections;
using System;

public class GameGridLogic : MonoBehaviour {
    private const int gridDimension = 6;
    private const float posXdifference = 0.013f;
    private static Vector3 digitPosDifference = new Vector3(0.014f, 0.0f, 0.0f);

    private SpriteRenderer cell;
    private SpriteRenderer[,] cells;
    private GameObject cellText;
    private GameObject[,] cellsText;


    private int[,] cellNumber;
    private int currentCellX, currentCellY;

	void Start () {
        // Initialize and allocate resources
        cell = Resources.Load<SpriteRenderer>("Prefabs/Square");
        cellText = Resources.Load<GameObject>("Prefabs/TextMesh");
        cells = new SpriteRenderer[gridDimension, gridDimension];
        cellsText = new GameObject[gridDimension, gridDimension];

        cellNumber = new int[gridDimension, gridDimension];

        float cellWidth, cellHeight;
        float spacingPercentage;
        float posX, posY;
        float textPosX, textPosY;

        spacingPercentage = 0.02f;
        cellWidth = (1 - (gridDimension + 1) * spacingPercentage) / gridDimension;
        cellHeight = (1 - (gridDimension + 1) * spacingPercentage) / gridDimension;

        for (int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                cells[i, j] = Instantiate<SpriteRenderer>(cell);
                cells[i, j].transform.parent = this.transform;
                cells[i, j].sortingLayerName = "Foreground";
                cells[i, j].transform.localScale = new Vector3(cellWidth, cellHeight, 1f);

                posX = -0.5f + (i * cellWidth + (i + 1) * spacingPercentage + cellWidth / 2);
                posY = 0.5f - (j * cellHeight + (j + 1) * spacingPercentage + cellHeight / 2);
                cells[i, j].transform.localPosition = new Vector2(posX, posY);

                cellsText[i, j] = Instantiate<GameObject>(cellText);
                MeshRenderer cellMeshRenderer = cellsText[i, j].GetComponent<MeshRenderer>();
                TextMesh cellTextMesh = cellsText[i, j].GetComponent<TextMesh>();

                cellMeshRenderer.sortingLayerName = "ForegroundText";
                cellMeshRenderer.transform.parent = this.transform;
                cellTextMesh.color = Color.black;

                textPosX = posX - (0.022f - 0.001f * gridDimension);
                textPosY = posY + (0.032f - 0.001f * gridDimension);
                cellTextMesh.fontSize = 32 - gridDimension;
                cellMeshRenderer.transform.localPosition = new Vector2(textPosX, textPosY);
                cellMeshRenderer.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            }

        currentCellX = 0;
        currentCellY = 0;
        cells[currentCellX, currentCellY].color = Color.green;
        generateCells();
    }

    public bool isMoveLegal(int x, int y) {
        if (x < 0 || x > gridDimension || y < 0 || y > gridDimension || cellNumber[x, y] <= 1)
            return false;
        return true;
    }

    public void generateCells() {
        System.Random rnd = new System.Random();

        for(int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                MeshRenderer meshRenderer = cellsText[i, j].GetComponent<MeshRenderer>();
                TextMesh textMesh = cellsText[i, j].GetComponent<TextMesh>();

                cellNumber[i, j] = rnd.Next(0, 20);
                textMesh.text = cellNumber[i, j].ToString();
                if (cellNumber[i, j] >= 10)
                    meshRenderer.transform.localPosition -= digitPosDifference;
                if (cellNumber[i, j] == 0) {
                    textMesh.text = "";
                    cells[i, j].color = Color.grey;
                }
            }
    }

    public void updateCell(int x, int y) {
        MeshRenderer meshRenderer = cellsText[x, y].GetComponent<MeshRenderer>();
        TextMesh textMesh = cellsText[x, y].GetComponent<TextMesh>();

        if (cellNumber[x, y] > 0) {
            --cellNumber[x, y];
            textMesh.text = cellNumber[x, y].ToString();
            if (cellNumber[x, y] == 9)
                meshRenderer.transform.localPosition += digitPosDifference;
            if (cellNumber[x, y] == 0) {
                textMesh.text = "";
                cells[x, y].color = Color.grey;
            }
        }
    }

    public void updateCells(int oldX, int oldY, int newX, int newY) {
        cells[oldX, oldY].color = Color.red;
        cells[newX, newY].color = Color.green;
        
        for (int i = 0; i < gridDimension; ++i)
            for (int j = 0; j < gridDimension; ++j) {
                updateCell(i, j);
            }
    }
	
	void Update () {
        Color red = new Color(255, 0, 0, 255);
        Color blue = new Color(0, 255, 0, 255);

        int newCellX = currentCellX;
        int newCellY = currentCellY;
        bool gotInput = false;


        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            newCellY -= 1;
            gotInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            newCellY += 1;
            gotInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            newCellX -= 1;
            gotInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            newCellX += 1;
            gotInput = true;
        }

        if (gotInput && isMoveLegal(newCellX, newCellY)) {
            updateCells(currentCellX, currentCellY, newCellX, newCellY);
            currentCellX = newCellX;
            currentCellY = newCellY;
        }
    }
}
