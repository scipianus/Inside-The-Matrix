using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class getStep : MonoBehaviour {

    // Use this for initialization
    private Text stepLabel;
    private int step = 0;
    private GameGridLogic gameLogic;
	void Start () {
        stepLabel = this.GetComponent<Text>();
        GameObject gameGrid = GameObject.Find("GameGrid");
        gameLogic = gameGrid.GetComponent<GameGridLogic>();
	}
	
	// Update is called once per frame
	void Update () {
        step = gameLogic.currentStep;
        stepLabel.text = string.Format(" {0}", step);	
	}
}
