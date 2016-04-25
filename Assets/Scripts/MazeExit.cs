using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MazeExit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.name == "Player") {
            Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
            float time = timer.time;
            float minutes = time / 60;
            float seconds = time % 60;
            GameOver.time = string.Format("{0:00}:{1:00}", minutes, seconds);
            GameOver.gameMode = 1;
            SceneManager.LoadScene("GameOver");
        }
    }
}
