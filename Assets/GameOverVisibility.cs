using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverVisibility : MonoBehaviour {

    // Use this for initialization
    public Text scoreText;
	void Start () {
        this.gameObject.active = false;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void becomeVisible(double score) {
        this.gameObject.active = true;
        scoreText.text = string.Format(" {0:0.00}", score);
    }
}
