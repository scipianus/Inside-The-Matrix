using UnityEngine;
using System.Collections;

public class Game2_KeyEvents : MonoBehaviour {
    private GameGridLogic gameGridLogic;

	void Start () {
        gameGridLogic = GameObject.Find("GameGrid").GetComponent<GameGridLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        Pair<int, int> currentCell = gameGridLogic.getCurrentCell();
        Pair<int, int> newCell = new Pair<int, int>(currentCell);
        int currentStep = gameGridLogic.getCurrentStep();
        bool gotInput = false;

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            newCell.fst -= 1;
            gotInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            newCell.fst += 1;
            gotInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            newCell.snd -= 1;
            gotInput = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            newCell.snd += 1;
            gotInput = true;
        }

        if (gotInput && gameGridLogic.isMoveLegal(newCell)) {
            gameGridLogic.setCurrentStep(currentStep + 1);
            gameGridLogic.updateCells(currentCell, newCell);
            gameGridLogic.setCurrentCell(newCell);
        }
	}
}
