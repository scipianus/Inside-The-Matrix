using UnityEngine;
using System.Collections;

public class DifficultyGame2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setGame2Difficulty(int diff) {
        GameGridLogic.gridDimension = diff;
    }
}
