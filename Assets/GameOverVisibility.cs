using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverVisibility : MonoBehaviour {
    public Text scoreText;

	void Start () {
        this.gameObject.active = false;	
	}
	
	void Update () {
	
	}

    public void becomeVisible(double score) {
        this.gameObject.active = true;
        scoreText.text = string.Format(" {0:0.00}", score);
    }
}
