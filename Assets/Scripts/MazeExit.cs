using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MazeExit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.name == "Player") {
            Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
			GameOver.time = timer.time;
            SceneManager.LoadScene("GameOver");
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
