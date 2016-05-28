using UnityEngine;
using System.Collections;

public class Cell {
    private SpriteRenderer spriteRenderer;
    private GameGridLogic gameGridLogic;
    private GameObject mesh;
    private MeshRenderer meshRenderer;
    private TextMesh textMesh;
    int gridDimension;
    int x, y;

    private static SpriteRenderer prefabCell = Resources.Load<SpriteRenderer>("Prefabs/Square");
    private static GameObject meshPrefab = Resources.Load<GameObject>("Prefabs/TextMesh");
    private static Vector3 digitPosDifference = new Vector3(0.014f, 0.0f, 0.0f);
    private static float spacingPercentage = 0.02f;

    public Cell(GameGridLogic gameGridLogic, int x, int y) {
        this.gameGridLogic = gameGridLogic;
        this.x = x;
        this.y = y;
        init();
    }

    private void init() {
        float cellWidth, cellHeight;
        float posX, posY;

        gridDimension = GameGridLogic.gridDimension;
        spriteRenderer = MonoBehaviour.Instantiate<SpriteRenderer>(prefabCell);
        mesh = MonoBehaviour.Instantiate<GameObject>(meshPrefab);
        meshRenderer = mesh.GetComponent<MeshRenderer>();
        textMesh = mesh.GetComponent<TextMesh>();
        cellWidth = (1 - (gridDimension + 1) * spacingPercentage) / gridDimension;
        cellHeight = (1 - (gridDimension + 1) * spacingPercentage) / gridDimension;

        spriteRenderer.transform.parent = gameGridLogic.transform;
        spriteRenderer.sortingLayerName = "Foreground";

        posX = -0.5f + (y * cellWidth + (y + 1) * spacingPercentage + cellWidth / 2);
        posY = 0.5f - (x * cellHeight + (x + 1) * spacingPercentage + cellHeight / 2);

        setLocalScale(new Vector3(cellWidth, cellHeight, 1f));
        setLocalPosition(new Vector3(posX, posY, 0f));

        meshRenderer.sortingLayerName = "ForegroundText";
        meshRenderer.transform.parent = gameGridLogic.transform;
        meshRenderer.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        textMesh.color = Color.black;
        textMesh.fontSize = 32 - gridDimension;
    }

    public void setLocalScale(Vector3 scale) {
        spriteRenderer.transform.localScale = scale;
    }

    public void setLocalPosition(Vector3 position) {
        spriteRenderer.transform.localPosition = position;
    }

    public void setText(string text) {
        float posX = spriteRenderer.transform.localPosition[0] - (0.022f - 0.001f * gridDimension);
        float posY = spriteRenderer.transform.localPosition[1] + (0.032f - 0.001f * gridDimension);
        int number;

        textMesh.text = text;
        meshRenderer.transform.localPosition = new Vector3(posX, posY, 0.0f);
        if (int.TryParse(text, out number) == true) {
            if (number > 9)
                meshRenderer.transform.localPosition -= digitPosDifference;
            else if (number == -1) {
                textMesh.text = "";
                setColor(Color.grey);
            }
        }
    }

    public void setColor(Color color) {
        spriteRenderer.color = color;
    }
}