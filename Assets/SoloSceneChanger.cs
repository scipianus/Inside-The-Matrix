using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoloSceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void changeScene(string name) {
        SceneManager.LoadScene(name);
    }
}
